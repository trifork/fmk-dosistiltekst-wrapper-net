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

namespace fmk_dosistiltekst_wrapper_net.ns2009 {


    public class LimitedNumberOfDaysConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test4Stk2GangeDagligI3DageVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
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
                    "DefaultLongTextConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og ophører efter det angivne forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 4 stk 2 gange\n" +
                    "   Søndag den 2. januar 2011: 4 stk 2 gange\n" +
                    "   Mandag den 3. januar 2011: 4 stk 2 gange\n" +
                    "   Tirsdag den 4. januar 2011: 4 stk 2 gange.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4 stk 2 gange daglig i 4 dage (gentages ikke).\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    8.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test4Til6Stk2GangeDagligI3DageVedMaaltid()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(4.0, 6.0),
                            PlainDoseWrapper.MakeDose(4.0, 6.0)),
                        DayWrapper.MakeDay(
                            2,
                            PlainDoseWrapper.MakeDose(4.0, 6.0),
                            PlainDoseWrapper.MakeDose(4.0, 6.0)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(4.0, 6.0),
                            PlainDoseWrapper.MakeDose(4.0, 6.0)))));
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og ophører efter det angivne forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Lørdag den 1. januar 2011: 4-6 stk 2 gange\n" +
                    "   Søndag den 2. januar 2011: 4-6 stk 2 gange\n" +
                    "   Mandag den 3. januar 2011: 4-6 stk 2 gange.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "4-6 stk 2 gange daglig i 3 dage (gentages ikke).\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    8.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MinimumValue,
                    0.000000001);
            Assert.AreEqual(
                    12.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Interval.MaximumValue,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testCQ611()  {
		DosageWrapper dosage = DosageWrapper.MakeDosage(
			StructuresWrapper.MakeStructures(
				UnitOrUnitsWrapper.MakeUnit("ml"), 
				StructureWrapper.MakeStructure(
					0, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null, 
					DayWrapper.MakeDay(
						3, 
						TimedDoseWrapper.MakeDose(new LocalTime(11,25), 7.0, false)))));
		Assert.AreEqual(
				"Doseringsforløbet starter lørdag den 1. januar 2011 og ophører efter det angivne forløb:\n" +
				"   Doseringsforløb:\n" +
				"   Mandag den 3. januar 2011: 7 ml kl. 11:25.\n   Bemærk: ved måltid",
				DosisTilTekstWrapper.ConvertLongText(dosage));
		Assert.IsNull(DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
		Assert.AreEqual(
				7.0/3.0, 
				DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 
				0.000000001); 							
		Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
	}

        // FMK-3017 Dosis-to-text oversætter NotIterated struktur med en enkelt dag forkert
        [Test]
        public void test5ml4gange()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("ml"),
                    StructureWrapper.MakeStructure(
                        0, null,
                        DateOrDateTimeWrapper.MakeDate("2010-01-01"), DateOrDateTimeWrapper.MakeDate("2110-01-01"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(5.0, true),
                            PlainDoseWrapper.MakeDose(5.0, true),
                            PlainDoseWrapper.MakeDose(5.0, true),
                            PlainDoseWrapper.MakeDose(5.0, true)))));

            Assert.AreEqual(
                    "5 ml efter behov 4 gange (gentages ikke)",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
        }

    }
}
