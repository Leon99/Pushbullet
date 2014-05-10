$packageName = "pushbulletconsole"
$zipName = "pushbulletconsole.zip"
$scriptPath = $(Split-Path -parent $MyInvocation.MyCommand.Definition)
$zipPath = Join-Path $scriptPath $zipName
$installPath = Join-Path $env:LOCALAPPDATA "LAV\PushbulletConsole"

Get-ChocolateyUnzip "$zipPath" "$installPath"
Remove-Item "$zipPath"

$batName = "pushbullet.bat"
$batPath = Join-Path $scriptPath $batName
Move-Item $batPath $(Join-Path $env:ChocolateyInstall "bin") -Force

echo "*******************************************************"
echo "*   Now simply type 'pushbullet -h' to get started.   *"
echo "*******************************************************"