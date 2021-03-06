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
            AssertLongTextEquals(dosage);
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
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "3 stk 2 gange daglig.\nBemærk: ved måltid",
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
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk daglig.\nBemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
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
            AssertLongTextEquals(dosage);
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
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk hver 2. dag.\nBemærk: ved måltid",
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk lørdag hver uge.\nBemærk: ved måltid", 
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1 stk 1 gang om måneden.\nBemærk: ved måltid", 
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk lørdag hver uge.\nBemærk: ved måltid", 
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 2 gange lørdag hver uge.\nBemærk: ved måltid",
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 1 gang om måneden.\nBemærk: ved måltid", 
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk 2 gange samme dag 1 gang om måneden.\nBemærk: ved måltid", 
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"2,5 stk hver 5. dag.\nBemærk: ved måltid", 
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
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2,5 stk 2 gange samme dag hver 5. dag.\nBemærk: ved måltid",
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
		AssertLongTextEquals(dosage);
		Assert.AreEqual(
				"RepeatedConverterImpl", 
				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				"1/2 stk 2 gange samme dag hver 5. dag.\nBemærk: ved måltid", 
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
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "RepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 stk kl. 8:00 daglig",
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


            AssertLongTextEquals(dosage);
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


            AssertLongTextEquals(dosage);
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

            AssertLongTextEquals(dosage);
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


            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage)); // does not have a short text translation
            Assert.AreEqual(
                    0.047619048,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
