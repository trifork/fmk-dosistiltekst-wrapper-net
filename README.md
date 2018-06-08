fmk-dosistiltekst-wrapper-net
==============
fmk-dosistiltekst-wrapper-net udstiller et .NET API der indkapsler dosistiltekst javascript komponenten (se <https://www.npmjs.com/package/fmk-dosis-til-tekst-ts> ). Denne version kan anvendes fra .NET 4.0 og derover.

Komponenten kan hentes som NuGet package med navn FMKDosisTilTekstWrapper ( <https://www.nuget.org/packages/FMKDosisTilTekstWrapper> ).

JS komponenten selv kan hentes vha. "npm i fmk-dosis-til-tekst-ts". Javascriptfilen dosistiltekst.js findes herefter i node_modules/fmk-dosis-til-tekst-ts/target folderen.

Før brug skal DosisTilTekstWrapper klassen initialiseres med en StreamReader indeholdende dosistiltekst.js filen, eksempelvis:
```C#
DosisTilTekstWrapper.Initialize(File.OpenText("node_modules/fmk-dosis-til-tekst-ts/target/dosistiltekst.js"));
```

Herefter kan de respektive hjælpe-metoder anvendes. De kaldes alle med en DosageWrapper instans som argument. DosageWrapper klasserne er identiske med de tidligere wrapper-klasser kendt fra den gamle dosis-til-tekst komponent, kun med ændret namespace. Det burde derfor være en forholdsvis enkel kodeopgave at skifte fra den gamle komponent til den nye, vha. dette projekt.

### Doseringsoversættelse

Eksempel på anvendelse, doseringsoversættelse:

```C#
DosageWrapper dosage = DosageWrapper.MakeDosage(
        StructuresWrapper.MakeStructures(
            UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"), 
            StructureWrapper.MakeStructure(1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"), 
                DayWrapper.MakeDay(1, PlainDoseWrapper.MakeDose(1.0), 
                    PlainDoseWrapper.MakeDose(1.0), 
                    PlainDoseWrapper.MakeDose(1.0, true)
                )
            )
        )
    );
  
string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
string mediumText = DosisTilTekstWrapper.ConvertShortText(dosage, 200);
DailyDosis daily = DosisTilTekstWrapper.CalculateDailyDosis(dosage);
DosageType dosageType = DosisTilTekstWrapper.GetDosageType(dosage);
```
Desuden er der også mulighed for at hente kort og lang tekst + daglig dosis hhv. kombineret for fler-periode strukturerede doseringer, samt for hver enkelt periode:
```C#
DosageTranslationCombined combined = DosisTilTekstWrapper.convertCombined(dosage);
```
### XML generering ud fra doseringsforslag

Eksempel på anvendelse:
```C#

DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("PN", "1", "1", "tablet", "tabletter", ", tages med rigeligt vand", 
    new [] { new DateTime(2017, 5, 17) }, new [] { new DateTime?(new DateTime(2017, 6, 1)) }, FMKVersion.FMK146, 1);

string xml = res.XmlSnippet;
string longText = res.LongText;
string shortText = res.ShortText;
```

Eksempel på anvendelse uden doserings-slutdato:
```C#

DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("PN", "1", "1", "tablet", "tabletter", ", tages med rigeligt vand", 
    new [] { new DateTime(2017, 5, 17) }, new DateTime?[] { null }, FMKVersion.FMK146, 1);
```

Eksempel på anvendelse med flere doseringsperioder:
```C#
 DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("{M+M+A+N}{PN}{N daglig}", "{1}{2}{1}",
        "{1+2+3+4}{dag 1: 2 dag 2: 3}{2}", "tablet", "tabletter", "tages med rigeligt vand",
        new [] { new DateTime(2010, 1, 1), new DateTime(2010, 2, 1), new DateTime(2010, 3, 1) },
        new[] { new DateTime?(new DateTime(2010, 1, 31)), new DateTime?(new DateTime(2010, 2, 28)), new DateTime?((new DateTime(2010, 3, 31)) },
        FMKVersion.FMK146, 1);
```		

Eksempel på anvendelse med flere doseringsperioder og med længere kort doseringstekst end FMK-snitfladens sædvanlige 70 karakterer:
```C#
 DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("{M+M+A+N}{PN}{N daglig}", "{1}{2}{1}",
        "{1+2+3+4}{dag 1: 2 dag 2: 3}{2}", "tablet", "tabletter", "tages med rigeligt vand",
        new [] { new DateTime(2010, 1, 1), new DateTime(2010, 2, 1), new DateTime(2010, 3, 1) },
        new[] { new DateTime?(new DateTime(2010, 1, 31)), new DateTime?(new DateTime(2010, 2, 28)), new DateTime?(new DateTime(2010, 3, 31)) },
        FMKVersion.FMK146, 1, 10000);
```				
