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

    public class LimitedNumberOfDaysConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("måleskefuld", "måleskefulde"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-04"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(4.0),
                            PlainDoseWrapper.MakeDose(4.0)),
                        DayWrapper.MakeDay(
                            2,
                            PlainDoseWrapper.MakeDose(4.0),
                            PlainDoseWrapper.MakeDose(4.0)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(4.0),
                            PlainDoseWrapper.MakeDose(4.0)),
                        DayWrapper.MakeDay(
                            4,
                            PlainDoseWrapper.MakeDose(4.0),
                            PlainDoseWrapper.MakeDose(4.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, og ophører tirsdag den 4. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 4 måleskefulde 2 gange ved måltid\n" +
                    "   Søndag den 2. januar 2011: 4 måleskefulde 2 gange ved måltid\n" +
                    "   Mandag den 3. januar 2011: 4 måleskefulde 2 gange ved måltid\n" +
                    "   Tirsdag den 4. januar 2011: 4 måleskefulde 2 gange ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4 måleskefulde 2 gange daglig i 4 dage ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    8.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("måleskefuld", "måleskefulde"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-04"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(4.0, true),
                            PlainDoseWrapper.MakeDose(4.0, true)),
                        DayWrapper.MakeDay(
                            2,
                            PlainDoseWrapper.MakeDose(4.0, true),
                            PlainDoseWrapper.MakeDose(4.0, true)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(4.0, true),
                            PlainDoseWrapper.MakeDose(4.0, true)),
                        DayWrapper.MakeDay(
                            4,
                            PlainDoseWrapper.MakeDose(4.0, true),
                            PlainDoseWrapper.MakeDose(4.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, og ophører tirsdag den 4. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 4 måleskefulde efter behov højst 2 gange ved måltid\n" +
                    "   Søndag den 2. januar 2011: 4 måleskefulde efter behov højst 2 gange ved måltid\n" +
                    "   Mandag den 3. januar 2011: 4 måleskefulde efter behov højst 2 gange ved måltid\n" +
                    "   Tirsdag den 4. januar 2011: 4 måleskefulde efter behov højst 2 gange ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4 måleskefulde efter behov 2 gange daglig i 4 dage ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testJustOne()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(4.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og ophører efter det angivne forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 4 tabletter ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4 tabletter 1 gang ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(4.0, DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 0.000000001);
            Assert.AreEqual(DosageType.OneTime, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
