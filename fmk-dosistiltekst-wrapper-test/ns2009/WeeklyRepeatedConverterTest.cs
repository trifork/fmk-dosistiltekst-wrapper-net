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

    public class WeeklyRepeatedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testWeeklyTwiceEveryTwoDays()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2012-06-08"), DateOrDateTimeWrapper.MakeDate("2012-12-31"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter fredag den 8. juni 2012, forløbet gentages hver uge, og ophører mandag den 31. december 2012.\n" +
                    "Bemærk at doseringen har et komplekst forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Tirsdag: 1 stk 2 gange\n" +
                    "   Torsdag: 1 stk 2 gange\n" +
                    "   Fredag: 1 stk 2 gange\n" +
                    "   Søndag: 1 stk 2 gange.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            //		Assert.AreEqual(
            //				dk.medicinkortet.fmkdosistiltekstwrapper.shorttextconverterimpl."WeeklyRepeatedConverterImpl", 
            //				DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            //		Assert.AreEqual("1 stk 2 gange daglig tirsdag, torsdag, fredag og søndag hver uge ved måltid", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    8 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testWeeklyOnceEveryTwoDays()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "ved måltid", DateOrDateTimeWrapper.MakeDate("2012-06-08"), DateOrDateTimeWrapper.MakeDate("2012-12-31"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter fredag den 8. juni 2012, forløbet gentages hver uge, og ophører mandag den 31. december 2012.\n" +
                    "Bemærk at doseringen har et komplekst forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Tirsdag: 1 stk\n" +
                    "   Torsdag: 1 stk\n" +
                    "   Fredag: 1 stk\n" +
                    "   Søndag: 1 stk.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual("1 stk tirsdag, torsdag, fredag og søndag hver uge ved måltid", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    4 / 7.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
