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
SET LoginKey=b0e5a8eb22c05cc8709eefb88801b91b14a86e1f
REM Define a list of unittest projects (.dll) seperated by a space
SET TestDlls="Implementation.UnitTests\bin\debug\Implementation.UnitTests.dll Crosscutting.Contracts.UnitTests\bin\debug\Crosscutting.Contracts.UnitTests.dll Crosscutting.Validators.UnitTests\bin\debug\Crosscutting.Validators.UnitTests.dll"

REM Remove previous testresults and coverage
rd /s /q "%CD%\TestResults"
md "%CD%\TestResults"

REM Start analyzer
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"%Solution%" /d:sonar.organization="%Organisation%" /d:sonar.host.url="%Host%" /d:sonar.login="%LoginKey%" /d:sonar.cs.vstest.reportsPaths="%CD%\TestResults\*.trx" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\TestResults\*.coveragexml"

REM (re)Build solution
MsBuild.exe /t:Rebuild SOLID.sln

REM Execute tests and collect coverage with "Dynamic Code Coverage Tools\CodeCoverage.exe" (hint: read statement from right to left)
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\\TestResults\VisualStudio.coverage" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "/Logger:trx" "%TestDlls%"
REM Save coverage as xml
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\\TestResults\VisualStudio.coveragexml" "%CD%\TestResults\VisualStudio.coverage"

REM Stop analyzing and generate/deploy report
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="%LoginKey%"
