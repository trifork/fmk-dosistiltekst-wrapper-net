/**
* The contents of this file are subject to the Mozilla Public
* License Version 1.1 (the "License"); you may not use this file
* except in compliance with the License. You may obtain a copy of
* the License at http://www.mozilla.org/MPL/
*
* Software distributed under the License is distributed on an "AS
* IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
* implied. See the License for the specific language governing
* rights and limitations under the License.
*
* Contributor(s): Contributors are attributed in the source code
* where applicable.
*
* The Original Code is "Dosis-til-tekst".
*
* The Initial Developer of the Original Code is Trifork Public A/S.
*
* Portions created for the FMK Project are Copyright 2011,
* National Board of e-Health (NSI). All Rights Reserved.
*/

namespace fmk_dosistiltekst_wrapper_net.ns20130601 {



using NUnit.Framework;


using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

    public class MultiplePeriodesTest : AbstractDosageWrapperTest
    {
        /*
        [Test]
        public void testWithNode() {
		  
                    ClientConfig config = new DefaultClientConfig();
                    Client client = Client.create(config);
                    WebResource service = client.resource("http://localhost:8080");
                    service.path("test");
		        
        }*/
        [Test]
        public void testTwoFollwingPeriodes()
        {

            //		for(int i = 0; i < 10000; i++) {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-3"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0))),


                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))
                        )
                    )
                );
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, og ophører mandag den 3. juni 2013.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013 og gentages hver dag:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
            //		}
        }

        [Test]
        public void testTwoFollwingPeriodes_Nedtrapning_DailyRepeated()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-03"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0))),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), DateOrDateTimeWrapper.MakeDate("2013-06-10"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))
                        )
                    )
                );
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, gentages hver dag, og ophører mandag den 3. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013, gentages hver dag, og ophører mandag den 10. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testTwoFollwingPeriodes_Nedtrapning_2DaysRepeated()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        2, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-03"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0))),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), DateOrDateTimeWrapper.MakeDate("2013-06-10"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))
                        )
                    )
                );
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, forløbet gentages hver 2. dag, og ophører mandag den 3. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   Dag 1: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013, gentages hver dag, og ophører mandag den 10. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testTwoFollwingPeriodes_Nedtrapning_3DaysRepeated()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        3, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-09"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(3.0))),
                    StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2013-06-09"), DateOrDateTimeWrapper.MakeDate("2013-06-18"),
                            DayWrapper.MakeDay(
                                1,
                                MorningDoseWrapper.MakeDose(2.0))),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-18"), DateOrDateTimeWrapper.MakeDate("2013-06-27"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))
                        )
                    )
                );
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, forløbet gentages efter 3 dage, og ophører søndag den 9. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 3 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter søndag den 9. juni 2013, gentages hver dag, og ophører tirsdag den 18. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 18. juni 2013, gentages hver dag, og ophører torsdag den 27. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testTwoFololwingPeriodesWithEmptyStructure()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-3"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0))),


                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), null)));
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, og ophører mandag den 3. juni 2013.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013:\n" +
                "   Bemærk: skal ikke anvendes i denne periode!",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testEmptyStructure()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-3"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0))),


                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), null)));
            Assert.AreEqual(
                "Doseringen indeholder flere perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, og ophører mandag den 3. juni 2013.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013:\n" +
                "   Bemærk: skal ikke anvendes i denne periode!",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }



        [Test]
        public void testTwoNotFollwingPeriodes()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0))),


                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))
                        )
                    )
                );
            Assert.AreEqual(
                "Doseringen indeholder flere perioder, bemærk at der er overlappende perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013 og ophører efter det angivne forløb.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013 og gentages hver dag:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        [Test]
        public void testTwoFollwingPeriodesWithOverlappingPN()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-3"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0))),


                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2013-06-04"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0))),


                    StructureWrapper.MakeStructure(
                        0, "ved smerter", DateOrDateTimeWrapper.MakeDate("2013-06-01"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(2.0, true))))

                );

            Assert.AreEqual(
                "Doseringen indeholder flere perioder, bemærk at der er overlappende perioder:\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013, og ophører mandag den 3. juni 2013.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                "\n" +
                "Doseringsforløbet starter lørdag den 1. juni 2013:\n" +
                "   Doseringsforløb:\n" +
                "   Efter behov: 2 tabletter efter behov.\n   Bemærk: ved smerter\n" +
                "\n" +
                "Doseringsforløbet starter tirsdag den 4. juni 2013 og gentages hver dag:\n" +
                "   Doseringsforløb:\n" +
                "   1 tablet morgen",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void freeTextWithStartEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                FreeTextWrapper.MakeFreeText(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-03"),
                    "Efter aftale"));

            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. juni 2013 og ophører mandag den 3. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Efter aftale",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("FreeTextConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void structuredWithStartEndDateTimeTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDateTime("2013-06-01 08:00:00"), DateOrDateTimeWrapper.MakeDateTime("2013-06-03 10:00:00"),
                            DayWrapper.MakeDay(
                                1,
                                MorningDoseWrapper.MakeDose(2.0),
                                NoonDoseWrapper.MakeDose(2.0),
                                EveningDoseWrapper.MakeDose(2.0)),
                            DayWrapper.MakeDay(
                                2,
                                MorningDoseWrapper.MakeDose(2.0),
                                EveningDoseWrapper.MakeDose(2.0)),
                            DayWrapper.MakeDay(
                                3,
                                MorningDoseWrapper.MakeDose(2.0))),


                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDateTime("2013-06-04 10:30:00"), DateOrDateTimeWrapper.MakeDateTime("2013-06-06 15:30:00"),
                            DayWrapper.MakeDay(
                                1,
                                MorningDoseWrapper.MakeDose(1.0))),


                        StructureWrapper.MakeStructure(
                            0, "ved smerter", DateOrDateTimeWrapper.MakeDateTime("2013-06-01 14:20:00"), DateOrDateTimeWrapper.MakeDateTime("2013-06-10 20:30:00"),
                            DayWrapper.MakeDay(
                                0,
                                PlainDoseWrapper.MakeDose(2.0, true))))

                    );


            Assert.AreEqual(
                    "Doseringen indeholder flere perioder, bemærk at der er overlappende perioder:\n" +
                    "\n" +
                    "Doseringsforløbet starter lørdag den 1. juni 2013 kl. 08:00, og ophører mandag den 3. juni 2013 kl. 10:00.\n" +
                    "Bemærk at doseringen varierer:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. juni 2013: 2 tabletter morgen + 2 tabletter middag + 2 tabletter aften\n" +
                    "   Søndag den 2. juni 2013: 2 tabletter morgen + 2 tabletter aften\n" +
                    "   Mandag den 3. juni 2013: 2 tabletter morgen\n" +
                    "\n" +
                    "Doseringsforløbet starter lørdag den 1. juni 2013 kl. 14:20:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 tabletter efter behov.\n   Bemærk: ved smerter\n" +
                    "\n" +
                    "Doseringsforløbet starter tirsdag den 4. juni 2013 kl. 10:30, gentages hver dag, og ophører torsdag den 6. juni 2013 kl. 15:30:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 tablet morgen",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void freeTextWithSameStartEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                FreeTextWrapper.MakeFreeText(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-01"),
                    "Efter aftale"));

            Assert.AreEqual(
                "Doseringen foretages kun lørdag den 1. juni 2013.\n" +
                "   Dosering:\n" +
                "   Efter aftale",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("FreeTextConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void freeTextWithStartDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                FreeTextWrapper.MakeFreeText(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), null,
                    "Efter aftale"));

            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Efter aftale",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("FreeTextConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void freeTextWithEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                FreeTextWrapper.MakeFreeText(
                    null, DateOrDateTimeWrapper.MakeDate("2013-06-03"),
                    "Efter aftale"));

            Assert.AreEqual(
                "Doseringsforløbet ophører mandag den 3. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Efter aftale",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("FreeTextConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void accordingToSchemaWithStartEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-03")));

            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. juni 2013 og ophører mandag den 3. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Dosering efter skriftlig anvisning",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("AdministrationAccordingToSchemaConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void accordingToSchemaWithSameStartEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), DateOrDateTimeWrapper.MakeDate("2013-06-01")));

            Assert.AreEqual(
                "Doseringen foretages kun lørdag den 1. juni 2013.\n" +
                "   Dosering:\n" +
                "   Dosering efter skriftlig anvisning",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("AdministrationAccordingToSchemaConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void accordingToSchemaWithStartDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema(
                    DateOrDateTimeWrapper.MakeDate("2013-06-01"), null));

            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Dosering efter skriftlig anvisning",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("AdministrationAccordingToSchemaConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void accordingToSchemaWithEndDateTest()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema(
                    null, DateOrDateTimeWrapper.MakeDate("2013-06-03")));

            Assert.AreEqual(
                "Doseringsforløbet ophører mandag den 3. juni 2013.\n" +
                "   Doseringsforløb:\n" +
                "   Dosering efter skriftlig anvisning",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("AdministrationAccordingToSchemaConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
