@ECHO ON
REM Download SonarQube Scanner for MSBuild at https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+MSBuild 
REM Change ScannerLocation to where SonarQube.Scanner.MSBuild.exe is located
REM Open a visual studio developer command prompt!
REM Change path to where SOLID.sln is located (eg. D:\Projects\SOLID)

REM Set execution directory of scanner utility
SET ScannerLocation=D:\SonarQube\Scanner

REM Setup
SET Host=https://sonarcloud.io
SET Organisation=k7o-github
SET Solution=SOLID
SET LoginKey=65f1d0ad38cb47bcd484a636014dbbbf7b191885
REM Define a list of unittest projects (.dll) seperated by a space
SET TestDlls="BusinessLogic.UnitTests\bin\debug\BusinessLogic.UnitTests.dll Dtos.UnitTests\bin\debug\Dtos.UnitTests.dll Crosscutting.Validators.UnitTests\bin\debug\Crosscutting.Validators.UnitTests.dll"

REM Remove previous testresults and coverage
rd /s /q "%CD%\TestResults"
md "%CD%\TestResults"

REM Start analyzer
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"%Solution%" /d:sonar.organization="%Organisation%" /d:sonar.host.url="%Host%" /d:sonar.login="%LoginKey%" /d:sonar.cs.vstest.reportsPaths="%CD%\TestResults\*.trx" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\TestResults\*.coveragexml"

REM (re)Build solution
dotnet.exe build

REM Execute tests and collect coverage with "Dynamic Code Coverage Tools\CodeCoverage.exe" (hint: read statement from right to left)
REM "%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\\TestResults\VisualStudio.coverage" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "/Logger:trx" "%TestDlls%"
REM Save coverage as xml
REM "%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\\TestResults\VisualStudio.coveragexml" "%CD%\TestResults\VisualStudio.coverage"

REM Stop analyzing and generate/deploy report
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="%LoginKey%"
