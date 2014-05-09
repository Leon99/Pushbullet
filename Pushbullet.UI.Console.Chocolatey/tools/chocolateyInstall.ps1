$packageName = 'pushbulletconsole'
$zipName = "pushbulletconsole.zip"
$scriptPath = $(Split-Path -parent $MyInvocation.MyCommand.Definition)
$zipPath = Join-Path $scriptPath $zipName

Get-ChocolateyUnzip "$zipPath" "$scriptPath"
Remove-Item "$zipPath"
echo "*******************************************************"
echo "*   Now simply type 'pushbullet -h' to get started.   *"
echo "*******************************************************"