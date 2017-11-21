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
    public class DailyRepeatedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1,
                        "ved måltid",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0, true)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 tablet ved måltid + 1 tablet ved måltid + 1 tablet efter behov ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "ParacetamolConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet 2-3 gange daglig ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1,
                        "ved måltid",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0, false),
                            PlainDoseWrapper.MakeDose(1.0, false),
                            PlainDoseWrapper.MakeDose(1.0, true)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 tablet ved måltid + 1 tablet ved måltid + 1 tablet efter behov ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "ParacetamolConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet 2-3 gange daglig ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        /* FMK-2444 Combined TimeDose/PlainDose was mistakenly converted by ParacetamolConverter */
        [Test]
        public void testCombinedTimePlain()
        {

            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1,
                        null,
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), null, //DateOrDateTimeWrapper.MakeDate("2011-01-01"), 
                        DayWrapper.MakeDay(
                            1,
                            TimedDoseWrapper.MakeDose(new LocalTime(8, 00), 1.0, false),
                            TimedDoseWrapper.MakeDose(new LocalTime(12, 00), 1.0, false),
                            TimedDoseWrapper.MakeDose(new LocalTime(16, 00), 1.0, false),
                            TimedDoseWrapper.MakeDose(new LocalTime(20, 00), 1.0, false),
                            PlainDoseWrapper.MakeDose(1.0, true),
                            PlainDoseWrapper.MakeDose(1.0, true),
                            PlainDoseWrapper.MakeDose(1.0, true)
                        )
                    )
                )
            );

            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 tablet kl. 08:00 + 1 tablet kl. 12:00 + 1 tablet kl. 16:00 + 1 tablet kl. 20:00 + 1 tablet efter behov + 1 tablet efter behov + 1 tablet efter behov",
                    DosisTilTekstWrapper.ConvertLongText(dosage));

            Assert.AreEqual(
                "DailyRepeatedConverterImpl",
                DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            string shorttext = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage, 1000));	/* no known converter */
            Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage)); /* no known converter */
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        // FMK-2606 DosageLong tekst viser ikke "1 gang daglig" i longtext
        [Test]
        public void testDailyRepeatedWithOneDailyDosage()
        {

            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnits("ml", "ml"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2013-01-01"), DateOrDateTimeWrapper.MakeDate("2013-08-15"),
                            DayWrapper.MakeDay(
                                1,
                                PlainDoseWrapper.MakeDose(12.0))
                            )
                    ));

            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            Assert.IsTrue(longText.Contains("12 ml 1 gang daglig"));
        }

    }
}