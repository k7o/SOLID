HOWTO: Register an EventSource on your system
NOTE: Relative paths used in this HOWTO are dependant on structure of solution the SOLID solution (https://github.com/k7o/SOLID). TIP: Point to the absolute locations of the manifest/dll's if they can't be found.

Run a debug build of the EventSources project

Open a powershell window (in administrator mode) and change directory to the \build dir of the Diagnostics.Tracing.EventRegister package (in current case \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build)

Execute following commands:

-- compiles and creates xml manifest
.\eventRegister.exe -DumpRegDlls ..\..\..\EventSources\bin\Debug\EventSources.dll -forceall=true

-- register the manifest and dll's on your system
wevtutil.exe im ..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.man /rf:"..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.dll" /mf:"..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.dll"

-- check if the manifest is correctly registered on you system
logman.exe -query providers | Select-String -Pattern '^QueryEventSource(.*)'

If you get a result like:

QueryEventSource                         {B187C1D9-3C15-53BD-B8AB-E986EB0A7520}

you're set, otherwise something went wrong!

Done!


Repeat this process for all providers you want to register


To remove a provider enter (relative to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build):

wevtutil.exe um ..\..\..\EventSources\bin\Debug\EventSources.QueryEventSource.etwManifest.man

