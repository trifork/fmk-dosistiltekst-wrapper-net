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
using fmk_dosistiltekst_wrapper_net.vowrapper;
using fmk_dosistiltekst_wrapper_test;

/**
 * Examples of translation to long dosage text, as discussed on the FMK-teknik forum: 
 * http://www.fmk-teknik.dk/index.php?topic=135.0
 */

namespace fmk_dosistiltekst_wrapper_net.ns2009 {


    public class LongTextComplexConverterTest : AbstractDosageWrapperTest
    {

        [Test] /* Dosage "1 tablet morgen" */
        public void test1TabletMorgen()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet morgen", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage "1 tablet morgen" with datetimes */
        public void test1TabletMorgenWithDatetimes()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                    1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-18 08:30:00"), null,
                    DayWrapper.MakeDay(
                        1,
                        MorningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet morgen", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Hjerdyl"-example */
        public void testHjerdyl()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                    2, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                    DayWrapper.MakeDay(
                        1,
                        MorningDoseWrapper.MakeDose(1.0)),
                    DayWrapper.MakeDay(
                        2,
                        MorningDoseWrapper.MakeDose(1.0),
                        EveningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Alendronat" example */
        public void testAledronat()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                    7, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                    DayWrapper.MakeDay(
                        1,
                        MorningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("WeeklyMorningNoonEveningNightConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("1 tablet morgen onsdag hver uge", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Marevan 14-dages skema 1+2 stk" example */
        public void testMarevan14DagesSkema1_2Tablet()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        14, null, DateOrDateTimeWrapper.MakeDate("2012-04-19"), null,
                        DayWrapper.MakeDay(
                            1, // torsdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3, // lørdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5, // mandag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            6,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7, // onsdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            8,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            9, // fredag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            10,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            11, // søndag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            12,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            13, // tirsdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            14,
                            MorningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Marevan ugeskema 1+2 stk" example */
        public void testMarevanUgeskema1_2Tabletter()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, null, DateOrDateTimeWrapper.MakeDate("2012-04-19"), null,
                        DayWrapper.MakeDay(
                            1, // torsdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3, // lørdag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5, // mandag
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            6,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7, // onsdag
                            MorningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    10 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Naragan ugeskema 1 tablet" example */
        public void testNaraganUgeskema1Tablet()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        7, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5,
                            MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7,
                            MorningDoseWrapper.MakeDose(1.0)))));

            AssertLongTextEquals(dosage);
            Assert.AreEqual("WeeklyMorningNoonEveningNightConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("1 tablet morgen tirsdag, onsdag, fredag og søndag hver uge", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    4 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Naragan ugeskema 2 tabletter" example */
        public void testNaraganUgeskema2Tabletter()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tabletter"),
                    StructureWrapper.MakeStructure(
                        7, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            6,
                            MorningDoseWrapper.MakeDose(2.0)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("WeeklyMorningNoonEveningNightConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("2 tabletter morgen mandag, torsdag og lørdag hver uge", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    6 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Morfin nedtrapning" example */
        public void testMorfinNedtrapning()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0),
                            NoonDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5,
                            MorningDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            6,
                            EveningDoseWrapper.MakeDose(1.0)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Pulmicort" example */
        public void testDag0Iterationsinterval0()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("sug"),
                    StructureWrapper.MakeStructure(
                        0, "ved anstrengelse", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            Assert.AreEqual(
                    "DefaultLongTextConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));

            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1-2 sug efter behov.\nBemærk: ved anstrengelse",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Ipren" example */
        public void testDag1Iterationsinterval1()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tabletter"),
                    StructureWrapper.MakeStructure(
                        1, "ved smerter", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true),
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true),
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage like the "Ipren" example, with a minor variation */
        public void testDag0Iterationsinterval1()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tabletter"),
                    StructureWrapper.MakeStructure(
                        1, "ved smerter", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Test dosage without meaning, this dosage must still be translated */
        public void test012Iterationsinterval0()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("ml"),
                    StructureWrapper.MakeStructure(
                        0, "mod smerter", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, true)),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(20.0, true),
                            PlainDoseWrapper.MakeDose(20.0, true)),
                        DayWrapper.MakeDay(
                            2,
                            PlainDoseWrapper.MakeDose(20.0, true),
                            PlainDoseWrapper.MakeDose(20.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Test dosage without meaning, must still be translated */
        public void testDag012Iterationsinterval2()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("sug"),
                    StructureWrapper.MakeStructure(
                        2, "ved anstrengelse", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)),
                        DayWrapper.MakeDay(
                            2,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Weekly dosage */
        public void testDag0Iterationsinterval7()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tabletter"),
                    StructureWrapper.MakeStructure(
                        7, "ved smerter", DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        [Test] /* Pure PN dosage */
        public void test1TabletEfterBehov()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2012-05-29"), null,
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0, true)))));
            
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet efter behov", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Pure PN dosage with max */
        public void test1TabletEfterBehovHoejstEnGangDaglig()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("tablet"),
                    StructureWrapper.MakeStructure(
                    1, null, DateOrDateTimeWrapper.MakeDate("2012-05-29"), null,
                    DayWrapper.MakeDay(
                        1,
                        PlainDoseWrapper.MakeDose(1.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet efter behov, højst 1 gang dagligt", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage "2 stk efter behov højst 1 gang daglig", see https://jira.trifork.com/browse/FMK-784*/
        public void testJiraFMK784()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:01"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.0, true)))));
            AssertLongTextEquals(dosage);   
            Assert.AreEqual("SimpleLimitedAccordingToNeedConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("2 stk efter behov, højst 1 gang dagligt", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Dosage "2 stk efter behov højst 2 gange daglig", see https://jira.trifork.com/browse/FMK-784*/
        public void testJiraFMK784Variant()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.0, true),
                            PlainDoseWrapper.MakeDose(2.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("SimpleLimitedAccordingToNeedConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("2 stk efter behov, højst 2 gange daglig", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test] /* Jira FMK-784 */
        public void testSecondsAreNotIncluded()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:00"), null,
                                    DayWrapper.MakeDay(
                                            1,
                                            PlainDoseWrapper.MakeDose(2.0, true),
                                            PlainDoseWrapper.MakeDose(2.0, true)))));
            AssertLongTextEquals(dosage);                                            
        }

        [Test] /* Jira FMK-784 */
        public void testSecondsAreIncludedWhenProvided()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:10"), null,
                                    DayWrapper.MakeDay(
                                            1,
                                            PlainDoseWrapper.MakeDose(2.0, true),
                                            PlainDoseWrapper.MakeDose(2.0, true)))));
            AssertLongTextEquals(dosage);
        }
    }	
}
