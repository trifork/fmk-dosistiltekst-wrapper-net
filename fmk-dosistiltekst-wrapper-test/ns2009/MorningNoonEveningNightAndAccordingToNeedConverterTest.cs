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

namespace fmk_dosistiltekst_wrapper_net.ns2009
{
    public class MorningNoonEveningNightAndAccordingToNeedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test1()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            NoonDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0),
                            NightDoseWrapper.MakeDose(2.0))),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.0, true)
                        )
                    )
                )
            );
            AssertLongTextEquals(dosage);
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test2()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(2.0, true),
                                MorningDoseWrapper.MakeDose(2.0, false)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
/*            Assert.AreEqual(
                    "MorningNoonEveningNightAndAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage)); */
            Assert.AreEqual(
                    "2 stk morgen, samt 2 stk efter behov, højst 1 gang dagligt",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test3()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                MorningDoseWrapper.MakeDose(1.0, 2.0),
                                NoonDoseWrapper.MakeDose(1.0, 1.0),
                                EveningDoseWrapper.MakeDose(1.0, 2.0),
                                PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test4()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-11"),
                            DayWrapper.MakeDay(1,
                                MorningDoseWrapper.MakeDose(1.0, 2.0),
                                NoonDoseWrapper.MakeDose(1.0, 1.0),
                                EveningDoseWrapper.MakeDose(1.0, 2.0),
                                PlainDoseWrapper.MakeDose(1.0, 2.0, true),
                                PlainDoseWrapper.MakeDose(1.0, 2.0, true)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}