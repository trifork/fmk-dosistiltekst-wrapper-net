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
    public class RepeatedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testUnits()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("dåse", "dåser"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(3.0),
                            PlainDoseWrapper.MakeDose(3.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   3 dåser 2 gange daglig",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "3 dåser 2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    6.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testAccordingToNeed()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("dåse", "dåser"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(3.0, true),
                            PlainDoseWrapper.MakeDose(3.0, true)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   3 dåser efter behov højst 2 gange daglig",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleLimitedAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "3 dåser efter behov, højst 2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testOnceWeekly()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2013-08-12"), null,
                        DayWrapper.MakeDay(
                            3,
                            TimedDoseWrapper.MakeDose(new LocalTime(8, 0), 1.0, false)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter mandag den 12. august 2013, forløbet gentages hver uge:\n" +
                    "   Doseringsforløb:\n" +
                    "   Onsdag: 1 stk kl. 08:00.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk kl. 08:00 onsdag hver uge.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testOnceWeekly2()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2013-08-12"), null,
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter mandag den 12. august 2013, forløbet gentages hver uge:\n" +
                    "   Doseringsforløb:\n" +
                    "   Onsdag: 1 stk morgen.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "WeeklyMorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk morgen onsdag hver uge.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        // FMK-2958 ASCP00078113 Dosis-til-text: slutdato mangler i ugentligt gentagede doseringer
        [Test]
        public void testOnceWeeklyWithEndDate()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2013-08-12"), DateOrDateTimeWrapper.MakeDate("2013-09-12"),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter mandag den 12. august 2013, forløbet gentages hver uge, og ophører torsdag den 12. september 2013:\n" +
                    "   Doseringsforløb:\n" +
                    "   Onsdag: 1 stk morgen.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "WeeklyMorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk morgen onsdag hver uge.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        [Test]
        public void testOnceEvery7thWeek()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					7*7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2013-08-12"), null, 
					DayWrapper.MakeDay(
						3, 
						PlainDoseWrapper.MakeDose(1.0)))));
		Assert.AreEqual(
				"DefaultLongTextConverterImpl", 
				DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
		Assert.AreEqual(
				"Doseringsforløbet starter mandag den 12. august 2013, forløbet gentages efter 49 dage:\n"+
				"   Doseringsforløb:\n"+
				"   Onsdag den 14. august 2013: 1 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl",
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk onsdag hver 7. uge.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				1/(7.0*7.0), 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 							
		Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));						
	}

        [Test]
        public void testOnceEvery2thMonth()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					60, "ved måltid", DateOrDateTimeWrapper.MakeDate("2013-08-12"), null, 
					DayWrapper.MakeDay(
						3, 
						PlainDoseWrapper.MakeDose(1.0)))));
		Assert.AreEqual(
				"DefaultLongTextConverterImpl", 
				DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
		Assert.AreEqual(
				"Doseringsforløbet starter mandag den 12. august 2013, forløbet gentages efter 60 dage:\n"+
				"   Doseringsforløb:\n"+
				"   Onsdag den 14. august 2013: 1 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl",
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk hver 2. måned.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				1/60.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 							
		Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));						
	}

        [Test]
        public void testMany()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-04"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(4.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører tirsdag den 4. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   4 tabletter 1 gang daglig.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4 tabletter 1 gang daglig.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(4.0, DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNew()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("plaster", "plastre"),
                    StructureWrapper.MakeStructure(
                        35, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-04"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            8,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            15,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 35 dage, og ophører tirsdag den 4. januar 2011.\n" +
                    "Bemærk at doseringen har et komplekst forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 1 plaster\n" +
                    "   Lørdag den 8. januar 2011: 1 plaster\n" +
                    "   Lørdag den 15. januar 2011: 1 plaster",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "DayInWeekConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 plaster daglig lørdag i de første 3 uger, herefter 2 ugers pause",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(3 / 35.0, DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

    }
}
