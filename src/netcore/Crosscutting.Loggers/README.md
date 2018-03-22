HOWTO: Register an EventSource on your system
NOTE: Relative paths used in this HOWTO are dependant on structure of solution the SOLID solution (https://github.com/k7o/SOLID). TIP: Point to the absolute locations of the manifest/dll's if they can't be found.

Run a debug build of the Crosscutting.Loggers project

Open a powershell window (in administrator mode) and change directory to the \build dir of the Diagnostics.Tracing.EventRegister package (in current case \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build)

Execute following commands:

-- compiles and creates xml manifest
.\eventRegister.exe -DumpRegDlls ..\..\..\Crosscutting.Loggers\bin\Debug\Crosscutting.Loggers.dll -forceall=true

-- register the manifest and dll's on your system
wevtutil.exe im ..\..\..\Crosscutting.Loggers\bin\Debug\Crosscutting.Loggers.k7o.TraceEventSource.etwManifest.man /rf:"..\..\..\Crosscutting.Loggers\bin\Debug\Crosscutting.Loggers.k7o.TraceEventSource.etwManifest.dll" /mf:"..\..\..\Crosscutting.Loggers\bin\Debug\Crosscutting.Loggers.k7o.TraceEventSource.etwManifest.dll"

-- check if the manifest is correctly registered on you system
logman.exe -query providers | Select-String -Pattern '^k7o.TraceEventSource(.*)'

If you get a result like:

k7o.TraceEventSource                         {B187C1D9-3C15-53BD-B8AB-E986EB0A7520}

you're set, otherwise something went wrong!

Done!


Repeat this process for all providers you want to register


To remove a provider enter (relative to \{SolutionDir}\packages\Microsoft.Diagnostics.Tracing.EventRegister.1.1.28\build):

wevtutil.exe um ..\..\..\Crosscutting.Loggers\bin\Debug\Crosscutting.Loggers.k7o.TraceEventSource.etwManifest.man

