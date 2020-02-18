Udviklet vha. Visual Studio Code med target framework netcoreapp 2.0  af hensyn til byggeserveren, og net4.0 til publicering vha. nuget. 

Åbn "Integrated terminal" i Visual Studio Code, og kør flg. heri:

Byg:
 cd fmk-dosistiltekst-wrapper-test
 dotnet build

Kør tests:
 cd fmk-dosistiltekst-wrapper-test
 dotnet test
 (test enkelt klasse: dotnet test --filter FullyQualifiedName~fmk_dosistiltekst_wrapper_net.ns2009.LimitedNumberOfDaysConverterTest)

Byg nuget pakke (fmk-dosistiltekst-wrapper-net\NetCore\bin\Debug\FMKDosisTilTekstWrapper.1.0.0.nupkg):
 cd fmk-dosistiltekst-wrapper-net
 dotnet pack /p:NuspecFile=fmk-dosistiltekst-wrapper-net.nuspec
 
 
Publicer på nuget.org (se api-key på https://wall.trifork.com/display/fmk/Dosis-til-tekst - VIGTIGT! API-KEY MÅ IKKE COMMIT'TES TIL GITHUB!):
 dotnet nuget push bin\Debug\FMKDosisTilTekstWrapper.X.X.X.nupkg -s nuget.org -k <api-key>

