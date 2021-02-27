
param([string]$file="")

function getScriptDirectory {
    Split-Path -Parent $PSCommandPath
}

function findVisualStudioInstallationPath {
    $programFilesDir = (Get-Item env:"ProgramFiles(x86)").Value
    $vsInstallerDir = Join-Path $programFilesDir "Microsoft Visual Studio\Installer"
    $vsWherePath = Join-Path $vsInstallerDir "vswhere.exe"
    $installationPath = (& $vsWherePath -latest -property installationPath)
    $installationPath
}

function findTextTransformExecutablePath {
    $vsPath = findVisualStudioInstallationPath
    Join-Path $vsPath "Common7\IDE\TextTransform.exe"
}

$root = getScriptDirectory
$textFransform = findTextTransformExecutablePath

Write-Host "Root: " $root
Write-Host "Text transform:" $textFransform

if ($file -ne "") {
    Write-Host "--- Transforming file: $file"
    & $textFransform -I $root $file
} else {
    Get-ChildItem -Path $root/*.tt -Recurse -Force | ForEach-Object {
        $relativePath = Get-Item $_ | Resolve-Path -Relative
        Write-Host "--- Transforming file: $relativePath"
        & $textFransform -I $root $_
    }
}
