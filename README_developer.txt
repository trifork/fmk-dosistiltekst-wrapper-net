Udviklet vha. Visual Studio Code med target framework netcoreapp 2.0 af hensyn til byggeserveren, og net4.0 til publicering vha. nuget. Desuden Visual Studio Express 13.0.
VSE 13 projekterne (.vcproj) ligger under hvert projekt, .net core under hver projekts NetCore folder.

Åbn "Integrated terminal" i Visual Studio Code, og kør flg. heri:

Byg:
 cd fmk-dosistiltekst-wrapper-test\NetCore
 dotnet build

Kør tests:
 cd fmk-dosistiltekst-wrapper-test\NetCore
 dotnet test

Byg nuget pakke (fmk-dosistiltekst-wrapper-net\NetCore\bin\Debug\FMKDosisTilTekstWrapper.1.0.0.nupkg):
 dotnet pack /p:NuspecFile=fmk-dosistiltekst-wrapper-net.nuspec
 
 
Publicer på nuget.org (se api-key på https://wall.trifork.com/display/fmk/Dosis-til-tekst - VIGTIGT! API-KEY MÅ IKKE COMMIT'TES TIL GITHUB!):
 dotnet nuget push bin\Debug\FMKDosisTilTekstWrapper.1.0.0.nupkg -k <api-key>

