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

using NUnit.Framework;
using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

namespace fmk_dosistiltekst_wrapper_net.ns2009
{
    public class MorningNoonEveningNightConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testMorningNoonEveningAndNight()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "!", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(3.0),
                            NightDoseWrapper.MakeDose(4.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 stk morgen + 2 stk middag + 3 stk aften + 4 stk nat.\n   Bemærk: !",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                "MorningNoonEveningNightConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 stk morgen, 2 stk middag, 3 stk aften og 4 stk nat.\n   Bemærk: !",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                10.0,
                DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testMorningNoonEveningAndNightWithEqualDoses()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0),
                            NightDoseWrapper.MakeDose(2.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   2 stk morgen + 2 stk middag + 2 stk aften + 2 stk nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                "MorningNoonEveningNightConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "2 stk morgen, middag, aften og nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    8.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNoonEveningAndNight()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(3.0),
                            NightDoseWrapper.MakeDose(4.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   2 stk middag + 3 stk aften + 4 stk nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                "MorningNoonEveningNightConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "2 stk middag, 3 stk aften og 4 stk nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    9.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testMorningNoonAndEvening()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(3.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 stk morgen + 2 stk middag + 3 stk aften.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 stk morgen, 2 stk middag og 3 stk aften.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    6.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testMorningAndNoonWithZerogetIntervals()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(0.0, 1.0),
                            NoonDoseWrapper.MakeDose(2.0, 3.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   0-1 stk morgen + 2-3 stk middag.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "0-1 stk morgen og 2-3 stk middag.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MinimumValue,
                    0.000000001);
            Assert.AreEqual(
                    4,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MaximumValue,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        public void testNight()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            NightDoseWrapper.MakeDose(2.0)))));
            Assert.AreEqual(
                "Daglig 2 stk nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "2 stk nat.\n   Bemærk: ved måltid",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1DråbeMiddagOgAften()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("dråber"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            NoonDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 dråbe middag + 1 dråbe aften",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 dråbe middag og aften",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1DråbeAftenOgNat()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("dråber"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            EveningDoseWrapper.MakeDose(1.0),
                            NightDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 dråbe aften + 1 dråbe nat",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 dråbe aften og nat",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1DråbeNat()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("dråber"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            NightDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 dråbe nat",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 dråbe nat",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test400MilligramNat()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("milligram"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            NightDoseWrapper.MakeDose(400.000)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   400 milligram nat",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "400 milligram nat",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    400.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* handle plurals, see https://jira.trifork.com/browse/FMK-943 */
        public void testJiraFMK943()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(2.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter tirsdag den 26. juni 2012 kl. 00:00 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 tablet morgen + 2 tabletter aften",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet morgen og 2 tabletter aften",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    3.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* handle plurals, see https://jira.trifork.com/browse/FMK-943 */
        public void testJiraFMK943Variant()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(0.0, 1.0),
                            EveningDoseWrapper.MakeDose(0.5, 1.5)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter tirsdag den 26. juni 2012 kl. 00:00 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   0-1 tablet morgen + 0,5-1,5 tabletter aften",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "0-1 tablet morgen og 1/2-1 1/2 tabletter aften",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MinimumValue,
                    0.000000001);
            Assert.AreEqual(
                    2.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MaximumValue,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* handle zero dosages stored in the database using the 2008-namespace, see https://jira.trifork.com/browse/FMK-872 */
        public void testJiraFMK872()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(0.0),
                            NoonDoseWrapper.MakeDose(42.117),
                            EveningDoseWrapper.MakeDose(0.0, 0.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter tirsdag den 26. juni 2012 kl. 00:00 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   42,117 tabletter middag + 0-0 tabletter aften",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "42,117 tabletter middag og 0-0 tabletter aften",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    42.117,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        [Test]
        public void testMorningFixedNightPN()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            null,
                            null,
                            NightDoseWrapper.MakeDose(1.0, true)
                        ))));

            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.AreEqual("1 tablet morgen, 1 tablet nat efter behov", shortText);
            Assert.IsTrue(longText.Contains("1 tablet morgen + 1 tablet nat efter behov"));
        }

        [Test]
        public void testMorningPnNightFixed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0, true),
                            null,
                            null,
                            NightDoseWrapper.MakeDose(1.0)
                        ))));

            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.AreEqual("1 tablet morgen efter behov, 1 tablet nat", shortText);
            Assert.IsTrue(longText.Contains("1 tablet morgen efter behov + 1 tablet nat"));
        }


        [Test]
        public void testMorningPnNoonFixedNightFixed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0, true),
                            NoonDoseWrapper.MakeDose(1.0, true),
                            EveningDoseWrapper.MakeDose(1.0, true),
                            NightDoseWrapper.MakeDose(1.0, true)
                        ))));

            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.IsNull(shortText); // Skulle have været "1 tablet morgen efter behov, 1 tablet middag efter behov, 1 tablet aften efter behov, 1 tablet nat efter behov" men for lang    
        }

        [Test]
        public void testMorningNoonEveningNightFixed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            NoonDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0),
                            NightDoseWrapper.MakeDose(1.0)
                        ))));

            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.AreEqual("1 tablet morgen, middag, aften og nat", shortText);
        }



        [Test]
        public void testMorningFixedNightPNDifferentQuantities()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-06-26 00:00:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            null,
                            null,
                            NightDoseWrapper.MakeDose(2.0, true)
                        ))));

            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.AreEqual("1 tablet morgen, 2 tabletter nat efter behov", shortText);
            Assert.IsTrue(longText.Contains("1 tablet morgen + 2 tabletter nat efter behov"));

        }
    }
}
