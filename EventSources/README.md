Register an EventSource on your system if you want to capture it



Open a cmd window (in administrator mode) and change directory to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build

Execute following commands:

.\eventRegister.exe -DumpRegDlls ..\..\..\EventSources\bin\Debug\EventSources.dll -forceall=true

wevtutil.exe im ..\..\..\EventSources\bin\Debug\EventSources.ClassLibraryEventSource.etwManifest.man /rf:"..\..\..\EventSources\bin\Debug\EventSources.ClassLibraryEventSource.etwManifest.dll" /mf:"..\..\..\EventSources\bin\Debug\EventSources.ClassLibraryEventSource.etwManifest.dll"

logman -query providers > providers.txt


Open the providers.csv in notepad and check if ClassLibraryEventSource is present, if not something went wrong!

Done!






To remove the provider enter (relative to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build)

wevtutil.exe um ..\..\..\EventSources\bin\Debug\EventSources.ClassLibraryEventSource.etwManifest.man

