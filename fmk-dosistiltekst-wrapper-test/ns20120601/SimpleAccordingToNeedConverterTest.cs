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

/**
 * The purpose of this test class is to test new functionality added in FMK 1.4 (2012/06/01 namespace). 
 * The test of the general functionality is done in the testclass of the same name in the 
 * dk.medicinkortet.fmkdosistiltekstwrapper.ns2009 package. 
 */

namespace fmk_dosistiltekst_wrapper_net.ns20120601
{
    public class SimpleAccordingToNeedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 tabletter efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 tabletter efter behov (gentages ikke)",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                        StructureWrapper.MakeStructure(
                            0, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 tabletter efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 tabletter efter behov (gentages ikke)",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2014-01-01"), DateOrDateTimeWrapper.MakeDate("2014-12-31"),
                            DayWrapper.MakeDay(0,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter onsdag den 1. januar 2014, gentages hver dag, og ophører onsdag den 31. december 2014:\n" +
                    "   Doseringsforløb:\n" +
                    "   Efter behov: 2 tabletter efter behov 2 gange",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingMmanToNeed()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                MorningDoseWrapper.MakeDose(2.0, true),
                                EveningDoseWrapper.MakeDose(2.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 11. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   2 tabletter morgen efter behov + 2 tabletter aften efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 tabletter morgen efter behov og aften efter behov",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
