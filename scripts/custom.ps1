function LoadCustomProperties {
    # TODO: Override these in private.ps1
    $script:FtpHost = "ftp.example.com"
    $script:FtpUser = "FtpUser"
    $script:FtpPassword = "FtpPassword"
    $script:FtpFolder = "public_html"
}

function CustomPublish {
    "Custom publish in progress!"
    $localFolder = "$collectDir\Uncas.NowSite.Web"
    SynchronizeFoldersViaFtp $script:FtpHost $script:FtpUser $script:FtpPassword $localFolder $script:FtpFolder
    "Custom publish done!"
}

function PostInit {
    $raw = (git log --pretty=format:"%x3centry%x3e%x3cauthor%x3e%an%x3c/author%x3e%x3ccommit_date%x3e%cd %x3c/commit_date%x3e%x3csubject%x3e%s%x3c/subject%x3e%x3c/entry%x3e")
    $full = "<logs>$raw</logs>"
    $logs = [xml]$full
    $html = "<ul id='commits'>"
    foreach ($entry in $logs.logs.entry) {
      $subject = $entry["subject"].InnerText
      $date = $entry["commit_date"].InnerText
      $html += "`
  <li><div class='date'>$date</div><div class='subject'>$subject</div></li>"
    }
    $html += "`
</ul>"
    $outputFile = "$srcDir\Uncas.NowSite.Web\Views\Home\CommitHistory.cshtml"
    $html > $outputFile
    "Written commit history to $outputFile"
}
