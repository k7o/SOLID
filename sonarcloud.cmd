@ECHO ON
REM Download SonarQube Scanner for MSBuild at https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+MSBuild 
REM Change ScannerLocation to where SonarQube.Scanner.MSBuild.exe is located
REM Open developer command prompt
REM Change path to where SOLID.sln is located (eg. D:\Projects\SOLID)

SET ScannerLocation=D:\SonarQube\Scanner

rd /s /q "%CD%\TestResults"
md "%CD%\TestResults"

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"SOLID" /d:sonar.organization="k7o-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="40fa91f115e53de566d71615d86b922be70d9334" /d:sonar.cs.vstest.reportsPaths="%CD%\TestResults\*.trx" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\TestResults\*.coveragexml"

MsBuild.exe /t:Rebuild SOLID.sln

"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\\TestResults\VisualStudio.coverage" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /Logger:trx "Implementation.UnitTests\bin\debug\Implementation.UnitTests.dll" "Crosscutting.Contracts.UnitTests\bin\debug\Crosscutting.Contracts.UnitTests.dll"
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\\TestResults\VisualStudio.coveragexml" "%CD%\TestResults\VisualStudio.coverage"

%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="40fa91f115e53de566d71615d86b922be70d9334"
