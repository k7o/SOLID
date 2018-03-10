@ECHO ON
REM Download SonarQube Scanner for MSBuild at https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+MSBuild 
REM Change ScannerLocation to where SonarQube.Scanner.MSBuild.exe is located
REM Open developer command prompt
REM Change path to where SOLID.sln is located (eg. D:\Projects\SOLID)

SET ScannerLocation=D:\SonarQube

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"SOLID" /d:sonar.organization="k7o-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="08e3282026ede40b71ce47c6ad35710b5cf8bc11"

MsBuild.exe /t:Rebuild SOLID.sln

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="08e3282026ede40b71ce47c6ad35710b5cf8bc11"
