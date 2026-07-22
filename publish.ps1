# Publish the Maniraya website for server upload (IIS / FTP).
#
# NOTE: This is an ASP.NET *Website Project* (pages use CodeFile, not CodeBehind).
# It CANNOT be precompiled with aspnet_compiler / "Publish Web Site", because many
# pages share the same class name (e.g. dozens declare `class admin_UserReport`).
# Precompilation compiles everything into shared assemblies and those duplicate
# class names collide. On a real server IIS compiles each page on demand, so the
# correct way to publish is a plain file (xcopy) deployment of the source tree.
#
# Usage:
#   .\publish.ps1                 # full site (excludes node_modules etc.)
#   .\publish.ps1 -ChangesOnly    # only git-changed files (for incremental upload)

param(
    [switch]$ChangesOnly
)

$ErrorActionPreference = "Stop"
$root       = $PSScriptRoot
$src        = Join-Path $root "Maniraya"
$publishDir = if ($ChangesOnly) { Join-Path $root "publish-changes" } else { Join-Path $root "publish" }
$zipPath    = if ($ChangesOnly) { Join-Path $root "Maniraya-changes.zip" } else { Join-Path $root "Maniraya-publish.zip" }

if (-not (Test-Path $src)) { throw "Source website folder not found: $src" }

Write-Host "Cleaning previous publish output..."
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
New-Item -ItemType Directory -Path $publishDir -Force | Out-Null

# Dev / build-only folders — not needed on the server.
# node_modules: ~116 MB, not referenced by any aspx/master page.
# build/dist: gulp/webpack build output; live pages use assets/ and plugins/.
$excludeDirNames = @(
    "node_modules",
    "obj",
    ".vs",
    "build",
    "dist",
    "App_Data\PublishProfiles"
)

if ($ChangesOnly) {
    Write-Host "Collecting changed files from git..."
    Push-Location $root
    try {
        $paths = @()
        $paths += git diff --name-only HEAD -- Maniraya 2>$null
        $paths += git diff --cached --name-only -- Maniraya 2>$null
        $paths += git ls-files --others --exclude-standard -- Maniraya 2>$null
        $paths = $paths | Where-Object { $_ } | ForEach-Object { $_.Replace('\', '/') } | Sort-Object -Unique

        # Drop anything under excluded folders
        $paths = $paths | Where-Object {
            $rel = $_
            -not ($excludeDirNames | Where-Object { $rel -match [regex]::Escape("Maniraya/$_") -or $rel -match [regex]::Escape("Maniraya\$_") })
        }

        # Skip binaries that should not overwrite server Bin unless intentional
        $paths = $paths | Where-Object {
            $_ -notmatch '\.(pdb)$' -and
            $_ -notmatch '^Maniraya/Bin/' -and
            $_ -notmatch '^Maniraya\\Bin\\'
        }

        if (-not $paths -or $paths.Count -eq 0) {
            throw "No changed files under Maniraya/ to publish."
        }

        $copied = 0
        foreach ($rel in $paths) {
            $from = Join-Path $root ($rel -replace '/', [IO.Path]::DirectorySeparatorChar)
            if (-not (Test-Path -LiteralPath $from)) {
                Write-Host "  skip (missing): $rel"
                continue
            }
            # Strip Maniraya/ prefix so zip contents match site root layout
            $destRel = $rel -replace '^Maniraya[\\/]', ''
            $to = Join-Path $publishDir ($destRel -replace '/', [IO.Path]::DirectorySeparatorChar)
            $toDir = Split-Path $to -Parent
            if (-not (Test-Path $toDir)) { New-Item -ItemType Directory -Path $toDir -Force | Out-Null }
            Copy-Item -LiteralPath $from -Destination $to -Force
            $copied++
            Write-Host "  + $destRel"
        }
        Write-Host "Copied $copied changed file(s)."
    }
    finally {
        Pop-Location
    }
}
else {
    Write-Host "Copying full website (excluding unused folders)..."
    $excludeDirs = $excludeDirNames | ForEach-Object { Join-Path $src $_ }
    $excludeFiles = @(
        "website.publishproj",
        "*.publishproj",
        "*.user",
        "*.pdb"
    )

    $roboArgs = @($src, $publishDir, "/MIR", "/NFL", "/NDL", "/NJH", "/NP", "/R:1", "/W:1")
    foreach ($d in $excludeDirs)  { $roboArgs += @("/XD", $d) }
    foreach ($f in $excludeFiles) { $roboArgs += @("/XF", $f) }

    robocopy @roboArgs | Out-Null
    if ($LASTEXITCODE -ge 8) { throw "robocopy failed with exit code $LASTEXITCODE" }
}

Write-Host "Creating zip archive..."
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path (Join-Path $publishDir "*") -DestinationPath $zipPath -CompressionLevel Optimal

$fileCount = (Get-ChildItem $publishDir -Recurse -File | Measure-Object).Count
$sizeMB    = [math]::Round(((Get-ChildItem $publishDir -Recurse -File | Measure-Object -Property Length -Sum).Sum / 1MB), 1)

Write-Host ""
Write-Host "Publish complete$(if ($ChangesOnly) { ' (CHANGES ONLY)' } else { ' (full site, non-precompiled)' })."
Write-Host "  Folder : $publishDir"
Write-Host "  Zip    : $zipPath"
Write-Host "  Files  : $fileCount"
Write-Host "  Size   : $sizeMB MB"
Write-Host ""
if ($ChangesOnly) {
    Write-Host "Upload these files into the matching paths on the server site root."
    Write-Host "node_modules / build / dist were NOT included."
}
else {
    Write-Host "Upload the CONTENTS of the publish folder (or extract the zip) into the"
    Write-Host "site root on the server. Make sure the Bin folder and Web.config go too."
    Write-Host "Excluded: node_modules, build, dist, obj, .vs, *.pdb"
}
