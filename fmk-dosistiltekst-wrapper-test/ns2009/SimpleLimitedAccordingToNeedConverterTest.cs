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

    public class SimpleLimitedAccordingToNeedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test1pustVedAnfaldHoejst3GangeDaglig()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("pust"),
                        StructureWrapper.MakeStructure(
                            1, "ved anfald", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(1.0, true),
                                PlainDoseWrapper.MakeDose(1.0, true),
                                PlainDoseWrapper.MakeDose(1.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "SimpleLimitedAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 pust efter behov, højst 3 gange daglig.\nBemærk: ved anfald",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1pustVedAnfaldHoejst1GangDaglig()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("pust"),
                        StructureWrapper.MakeStructure(
                            1, "ved anfald", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(1.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "SimpleLimitedAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 pust efter behov, højst 1 gang dagligt.\nBemærk: ved anfald",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testStkEfterBehovHoejst1GangDaglig()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2012-06-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(1.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "SimpleLimitedAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk efter behov, højst 1 gang dagligt",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNoShortText()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDate("2012-06-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0)),
                            DayWrapper.MakeDay(2,
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0)),
                            DayWrapper.MakeDay(3,
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0)),
                            DayWrapper.MakeDay(4,
                                PlainDoseWrapper.MakeDose(1.0),
                                PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(null,
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
