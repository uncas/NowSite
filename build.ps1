$task = $args[0]

$psenVersion = "0.2.0.60"

$psenPath = ".\packages\psen.$psenVersion\tools\psen.ps1"
if (!(Test-Path $psenPath))
{
    .nuget\nuget.exe install psen -o packages -version $psenVersion
}

& $psenPath $task