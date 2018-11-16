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

namespace fmk_dosistiltekst_wrapper_net.ns20120601
{
    /**
     * The purpose of this test class is to test new functionality added in FMK 1.4 (2012/06/01 namespace). 
     * The test of the general functionality is done in the testclass of the same name in the 
     * dk.medicinkortet.fmkdosistiltekstwrapper.ns2009 package. 
     */
    public class MorningNoonEveningNightInNDaysConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-05"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0, false)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(1.0, false)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0, false)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(1.0, false)),
                        DayWrapper.MakeDay(
                            5,
                            MorningDoseWrapper.MakeDose(1.0, false)))));
            Assert.AreEqual(
                    "DefaultLongTextConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, og ophører onsdag den 5. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 1 tablet morgen\n" +
                    "   Søndag den 2. januar 2011: 1 tablet morgen\n" +
                    "   Mandag den 3. januar 2011: 1 tablet morgen\n" +
                    "   Tirsdag den 4. januar 2011: 1 tablet morgen\n" +
                    "   Onsdag den 5. januar 2011: 1 tablet morgen.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightInNDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet morgen ved måltid i 5 dage",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-05"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0, true)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(1.0, true)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0, true)),
                        DayWrapper.MakeDay(
                            4,
                            MorningDoseWrapper.MakeDose(1.0, true)),
                        DayWrapper.MakeDay(
                            5,
                            MorningDoseWrapper.MakeDose(1.0, true)))));
            Assert.AreEqual(
                    "DefaultLongTextConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, og ophører onsdag den 5. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 1 tablet morgen efter behov højst 1 gang\n" +
                    "   Søndag den 2. januar 2011: 1 tablet morgen efter behov højst 1 gang\n" +
                    "   Mandag den 3. januar 2011: 1 tablet morgen efter behov højst 1 gang\n" +
                    "   Tirsdag den 4. januar 2011: 1 tablet morgen efter behov højst 1 gang\n" +
                    "   Onsdag den 5. januar 2011: 1 tablet morgen efter behov højst 1 gang.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "MorningNoonEveningNightInNDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet morgen efter behov ved måltid i 5 dage",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

    }
}
