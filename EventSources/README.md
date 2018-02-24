The 



HOWTO: Register the EventSource on your system 


Open a cmd window (in administrator mode) and change directory to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build

Execute following commands:

.\eventRegister.exe -DumpRegDlls ..\..\..\EventSources\bin\Debug\EventSources.dll -forceall=true

wevtutil.exe im ..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.man /rf:"..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.dll" /mf:"..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.dll"

logman -query providers > providers.txt


Open the providers.csv in notepad and check if QueryEventSource is present, if not something went wrong!

Done!






To remove the provider enter (relative to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build)

wevtutil.exe um ..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.man

