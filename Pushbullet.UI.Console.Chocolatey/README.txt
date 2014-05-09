How to prepare the package:
- change version in PushbulletConsole.nuspec
- switch to Deployment configuration
- build this project

How to verify the package:
- set working directory to the project's directory
- run
cuninst pushbulletconsole
cinst pushbulletconsole -source "%cd%"

How to publish the package:
- set working directory to the project's directory
- run
cpush pushbulletconsole.<version>.nupkg