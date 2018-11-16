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

namespace fmk_dosistiltekst_wrapper_net.ns2009
{

    public class RepeatedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test3stk2gangeDaglig()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(3.0),
                            PlainDoseWrapper.MakeDose(3.0)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   3 stk 2 gange daglig",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "3 stk 2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    6.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stk3gangeDagligVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(3.0),
                            PlainDoseWrapper.MakeDose(3.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   3 stk 2 gange daglig.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "3 stk 2 gange daglig ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    6.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkDagligVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 stk 1 gang daglig.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk daglig ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            // assertEquals(10.0, result.getAvgDailyDosis().doubleValue()); 				
        }

        [Test]
        public void test1Til2stk2GangeDaglig()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0, 2.0),
                            PlainDoseWrapper.MakeDose(1.0, 2.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   1-2 stk 2 gange daglig",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1-2 stk 2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MinimumValue,
                    0.000000001);
            Assert.AreEqual(
                    4.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MaximumValue,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkHver2DagVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        2, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "TwoDaysRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages hver 2. dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   Dag 1: 1 stk.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk hver 2. dag ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }


        [Test]
        public void test1stkOmUgenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(1.0)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages hver uge, og ophører søndag den 30. januar 2011:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag: 1 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk lørdag hver uge ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				1/7.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 				
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));		
	}

        [Test]
        public void test1stkOmMaanedenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					30, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(1.0)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 30 dage, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag den 1. januar 2011: 1 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk 1 gang om måneden ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				1/30.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 				
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));		
	}

        [Test]
        public void test2_5stk1GangOmUgenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(2.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages hver uge, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag: 2,5 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk lørdag hver uge ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				2.5/7.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 				
	}

        [Test]
        public void test2_5Stk2GangeSammeDag1GangOmUgenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(2.5), 
						PlainDoseWrapper.MakeDose(2.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages hver uge, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag: 2,5 stk 2 gange.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 2 gange lørdag hver uge ved måltid",
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				5/7.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001);
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));		
	}

        [Test]
        public void test2_5stk1GangOmMaanedenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					30, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1,
						PlainDoseWrapper.MakeDose(2.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 30 dage, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag den 1. januar 2011: 2,5 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 1 gang om måneden ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				2.5/30.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001);
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));		
	}

        [Test]
        public void test2_5Stk2GangeSammeDag1GangOmMaanedenVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					30, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"),  
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(2.5), 
						PlainDoseWrapper.MakeDose(2.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 30 dage, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag den 1. januar 2011: 2,5 stk 2 gange.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 2 gange samme dag 1 gang om måneden ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				5/30.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 				
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));				
	}

        [Test]
        public void test2_5stkHver5DagVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					5, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(2.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 5 dage, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag den 1. januar 2011: 2,5 stk.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk hver 5. dag ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				2.5/5.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 			
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));				
	}

        [Test]
        public void test2_5stk2GangeSammeDagHver5DagVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        5, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.5),
                            PlainDoseWrapper.MakeDose(2.5)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 5 dage, og ophører tirsdag den 1. januar 2013:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 2,5 stk 2 gange.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2,5 stk 2 gange samme dag hver 5. dag ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test0_5stk1GangSammeDagHver5DagVedMaaltid()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("stk"), 
				StructureWrapper.MakeStructure(
					5, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2013-01-01"), 
					DayWrapper.MakeDay(
						1, 
						PlainDoseWrapper.MakeDose(0.5), 
						PlainDoseWrapper.MakeDose(0.5)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages efter 5 dage, og ophører tirsdag den 1. januar 2013:\n"+
				"   Doseringsforløb:\n"+
				"   Lørdag den 1. januar 2011: 0,5 stk 2 gange.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1/2 stk 2 gange samme dag hver 5. dag ved måltid", 
				DosisTilTekstWrapper.ConvertShortText(dosage));
		Assert.AreEqual(
				1/5.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 				
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));				
	}

        [Test]
        public void test1stkDagligKl0800()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            TimedDoseWrapper.MakeDose(new LocalTime(8, 00), 1.0, false)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
                    "   Doseringsforløb:\n" +
                    "   1 stk kl. 08:00",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk kl. 08:00 daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkHver6uger()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1,
                                            PlainDoseWrapper.MakeDose(1.0))
                            )
                    ));


            Assert.AreEqual("Doseringsforløbet starter fredag den 7. februar 2014 kl. 07:19, forløbet gentages efter 42 dage:\n" +
                    "   Doseringsforløb:\n" +
                    "   Fredag den 7. februar 2014: 1 stk", DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "1 stk fredag hver 6. uge",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.023809524,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkKl12Hver6uger()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1,
                                            TimedDoseWrapper.MakeDose(new LocalTime(12, 0), 1.0, false))
                            )
                    ));


            Assert.AreEqual("Doseringsforløbet starter fredag den 7. februar 2014 kl. 07:19, forløbet gentages efter 42 dage:\n" +
                    "   Doseringsforløb:\n" +
                    "   Fredag den 7. februar 2014: 1 stk kl. 12:00", DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "1 stk kl. 12:00 fredag hver 6. uge",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.023809524,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMiddagHver6uger()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1,
                                            NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));


            Assert.AreEqual("Doseringsforløbet starter fredag den 7. februar 2014 kl. 07:19, forløbet gentages efter 42 dage:\n" +
                    "   Doseringsforløb:\n" +
                    "   Fredag den 7. februar 2014: 1 stk middag", DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "1 stk middag fredag hver 6. uge",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.023809524,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgenOgNatHver6uger()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1,
                                            NoonDoseWrapper.MakeDose(1.0),
                                            NightDoseWrapper.MakeDose(1.0))
                            )
                    ));


            Assert.AreEqual("Doseringsforløbet starter fredag den 7. februar 2014 kl. 07:19, forløbet gentages efter 42 dage:\n" +
                    "   Doseringsforløb:\n" +
                    "   Fredag den 7. februar 2014: 1 stk middag + 1 stk nat", DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage)); // does not have a short text translation
            Assert.AreEqual(
                    0.047619048,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
