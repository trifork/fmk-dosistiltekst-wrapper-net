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
namespace fmk_dosistiltekst_wrapper_net.ns20120601 {

    public class LongTextComplexConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0, false)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet morgen", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            0,
                            MorningDoseWrapper.MakeDose(1.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 tablet morgen efter behov", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

      

        [Test]
        public void testDifferentFirstDosage()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                                TimedDoseWrapper.MakeDose(new LocalTime(10, 0), 1.0, false),

                                PlainDoseWrapper.MakeDose(2.0),
                                PlainDoseWrapper.MakeDose(2.0),
                                PlainDoseWrapper.MakeDose(2.0),
                                PlainDoseWrapper.MakeDose(2.0)
                            ))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(9, DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testFirstDosageDiffersOnPN()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2012-04-18"), null,
                        DayWrapper.MakeDay(
                            1,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                PlainDoseWrapper.MakeDose(2.0, false),
                                PlainDoseWrapper.MakeDose(2.0, false)
                            ))));
            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }	
}