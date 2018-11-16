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

namespace fmk_dosistiltekst_wrapper_net.ns2009 {
    public class ParacetamolConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test1Til2stk3Til4GangeDagligVedSmerter()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved smerter", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0, 2.0),
                            PlainDoseWrapper.MakeDose(1.0, 2.0),
                            PlainDoseWrapper.MakeDose(1.0, 2.0),
                            PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1-2 stk + 1-2 stk + 1-2 stk + 1-2 stk efter behov.\n   Bemærk: ved smerter",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                "ParacetamolConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1-2 stk 3-4 gange daglig ved smerter",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testFixedAndPNEquals()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, false)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 11. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   2 stk efter behov + 2 stk",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "ParacetamolConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk 1-2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testFixedAndPNEquals2()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, false),
                                PlainDoseWrapper.MakeDose(2.0, false)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 11. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   2 stk efter behov + 2 stk 2 gange daglig",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "ParacetamolConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk 2-3 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testFixedAndPNEquals3()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, false),
                                PlainDoseWrapper.MakeDose(2.0, false)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 11. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   2 stk efter behov + 2 stk efter behov + 2 stk + 2 stk",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "ParacetamolConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk 2-4 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testFixedAndPNDifferent()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0, true),
                                PlainDoseWrapper.MakeDose(2.0, false)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 11. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   4 stk efter behov + 2 stk",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

    }
}
