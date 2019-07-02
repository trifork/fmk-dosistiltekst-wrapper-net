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
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

namespace fmk_dosistiltekst_wrapper_net.ns2009
{

    public class SimpleAccordingToNeedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test2StkEfterBehov()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 stk efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk efter behov (gentages ikke)",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test2StkEfterBehovVedSmerter()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            0, "ved smerter", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 stk efter behov.\n   Bemærk: ved smerter",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk efter behov (gentages ikke).\n   Bemærk: ved smerter",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1Til2StkEfterBehov()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 1-2 stk efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1-2 stk efter behov (gentages ikke)",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
