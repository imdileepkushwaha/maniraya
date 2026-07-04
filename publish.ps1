# Publish the Maniraya website for server upload (IIS / FTP).
#
# NOTE: This is an ASP.NET *Website Project* (pages use CodeFile, not CodeBehind).
# It CANNOT be precompiled with aspnet_compiler / "Publish Web Site", because many
# pages share the same class name (e.g. dozens declare `class admin_UserReport`).
# Precompilation compiles everything into shared assemblies and those duplicate
# class names collide. On a real server IIS compiles each page on demand, so the
# correct way to publish is a plain file (xcopy) deployment of the source tree.
#
# This script mirrors the website into .\publish and produces Maniraya-publish.zip.

$ErrorActionPreference = "Stop"
$root       = $PSScriptRoot
$src        = Join-Path $root "Maniraya"
$publishDir = Join-Path $root "publish"
$zipPath    = Join-Path $root "Maniraya-publish.zip"

if (-not (Test-Path $src)) { throw "Source website folder not found: $src" }

Write-Host "Cleaning previous publish output..."
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
New-Item -ItemType Directory -Path $publishDir -Force | Out-Null

# Directories that are NOT needed on the server (dev / build only).
$excludeDirs = @(
    (Join-Path $src "node_modules"),                 # front-end build deps, not referenced by any page
    (Join-Path $src "obj"),
    (Join-Path $src ".vs"),
    (Join-Path $src "App_Data\PublishProfiles")      # publish tooling only
)

# Files that are NOT needed on the server.
$excludeFiles = @(
    "website.publishproj",
    "*.publishproj",
    "*.user",
    "*.pdb"
)

Write-Host "Copying website files to publish folder..."
$roboArgs = @($src, $publishDir, "/MIR", "/NFL", "/NDL", "/NJH", "/NP", "/R:1", "/W:1")
foreach ($d in $excludeDirs)  { $roboArgs += @("/XD", $d) }
foreach ($f in $excludeFiles) { $roboArgs += @("/XF", $f) }

robocopy @roboArgs | Out-Null
# robocopy uses bitmapped exit codes; anything >= 8 means a real failure.
if ($LASTEXITCODE -ge 8) { throw "robocopy failed with exit code $LASTEXITCODE" }

Write-Host "Creating zip archive..."
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path (Join-Path $publishDir "*") -DestinationPath $zipPath -CompressionLevel Optimal

$fileCount = (Get-ChildItem $publishDir -Recurse -File | Measure-Object).Count
$sizeMB    = [math]::Round(((Get-ChildItem $publishDir -Recurse -File | Measure-Object -Property Length -Sum).Sum / 1MB), 1)

Write-Host ""
Write-Host "Publish complete (non-precompiled / file deployment)."
Write-Host "  Folder : $publishDir"
Write-Host "  Zip    : $zipPath"
Write-Host "  Files  : $fileCount"
Write-Host "  Size   : $sizeMB MB"
Write-Host ""
Write-Host "Upload the CONTENTS of the publish folder (or extract the zip) into the"
Write-Host "site root on the server. Make sure the Bin folder and Web.config go too."
