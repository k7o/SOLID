@ECHO ON
REM Download SonarQube Scanner for MSBuild at https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+MSBuild 
REM Change ScannerLocation to where SonarQube.Scanner.MSBuild.exe is located
REM Open developer command prompt
REM Change path to where SOLID.sln is located (eg. D:\Projects\SOLID)

SET ScannerLocation=C:\SonarQube\Scanner

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"SOLID" /d:sonar.organization="k7o-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="e4ec4be24281c0058c579b0f01266a6936f1714e" /d:sonar.cs.vstest.reportsPaths="%CD%\TestResults\*.trx" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\VisualStudio.coveragexml"

MsBuild.exe /t:Rebuild SOLID.sln

"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\VisualStudio.coverage"

"%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /Logger:trx "Implementation.UnitTests\bin\debug\Implementation.UnitTests.dll" /EnableCodeCoverage
"%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /Logger:trx "Crosscutting.Contracts.UnitTests\bin\debug\Crosscutting.Contracts.UnitTests.dll" /collect:"Code Coverage"

"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\VisualStudio.coveragexml" "%CD%\VisualStudio.coverage"

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="e4ec4be24281c0058c579b0f01266a6936f1714e"
