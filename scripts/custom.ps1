function LoadCustomProperties {
    # TODO: Override these in private.ps1
    $script:FtpHost = "ftp.example.com"
    $script:FtpUser = "FtpUser"
    $script:FtpPassword = "FtpPassword"
    $script:FtpFolder = "new"
}

function CustomPublish {
    "Custom publish in progress!"
    $localFolder = "$collectDir\Uncas.NowSite.Web"
    SynchronizeFoldersViaFtp $script:FtpHost $script:FtpUser $script:FtpPassword $localFolder $script:FtpFolder
    "Custom publish done!"
}