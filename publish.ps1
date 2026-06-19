# Publish Maniraya website for server upload (IIS / FTP)
$ErrorActionPreference = "Stop"
$root = $PSScriptRoot
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
if (-not $msbuild) { throw "MSBuild not found. Install Visual Studio Build Tools." }

$publishUrl = Join-Path $root "publish"
if (Test-Path $publishUrl) { Remove-Item $publishUrl -Recurse -Force }
New-Item -ItemType Directory -Path $publishUrl -Force | Out-Null

& $msbuild (Join-Path $root "Maniraya\website.publishproj") `
    /p:DeployOnBuild=true `
    /p:PublishProfile=pubment `
    /p:Configuration=Debug `
    /p:Platform=AnyCPU `
    /p:publishUrl="$publishUrl" `
    /p:DeleteExistingFiles=True `
    /v:minimal

if ($LASTEXITCODE -ne 0) { throw "Publish failed with exit code $LASTEXITCODE" }

$zipPath = Join-Path $root "Maniraya-publish.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path (Join-Path $publishUrl "*") -DestinationPath $zipPath -CompressionLevel Optimal

Write-Host ""
Write-Host "Publish complete."
Write-Host "  Folder: $publishUrl"
Write-Host "  Zip:    $zipPath"
