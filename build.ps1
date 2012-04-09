$task = $args[0]

$psenVersion = "0.1.0.45"

$psenPath = ".\packages\psen.$psenVersion\tools\psen.ps1"
if (!(Test-Path $psenPath))
{
    .nuget\nuget.exe install psen -o packages -version $psenVersion
}

& $psenPath $task