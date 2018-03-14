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
SET LoginKey=36b8a72519d4a43f387d04d009ed212638d9b615

REM Remove previous testresults and coverage
rd /s /q "%CD%\TestResults"
md "%CD%\TestResults"

REM Start analyzer
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe begin /k:"%Solution%" /d:sonar.organization="%Organisation%" /d:sonar.host.url="%Host%" /d:sonar.login="%LoginKey%" /d:sonar.cs.vstest.reportsPaths="%CD%\TestResults\*.trx" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\TestResults\*.coveragexml"

REM (re)Build solution
MsBuild.exe /t:Rebuild SOLID.sln

REM Execute tests and collect coverage with "Dynamic Code Coverage Tools\CodeCoverage.exe" (hint: read statement from right to left)
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\\TestResults\VisualStudio.coverage" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "/Logger:trx" "Implementation.UnitTests\bin\debug\Implementation.UnitTests.dll" "Crosscutting.Contracts.UnitTests\bin\debug\Crosscutting.Contracts.UnitTests.dll"
REM Save coverage as xml
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\\TestResults\VisualStudio.coveragexml" "%CD%\TestResults\VisualStudio.coverage"

REM Stop analyzing and generate/deploy report
%ScannerLocation%\SonarQube.Scanner.MSBuild.exe end /d:sonar.login="%LoginKey%"
