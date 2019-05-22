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

    public class SimpleNonRepeatedConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test1Plaster5TimerFoerVirkningOenskes()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("plaster"),
                    StructureWrapper.MakeStructure(
                        0, "5 timer før virkning ønskes", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            0,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, og ophører søndag den 30. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   Dag ikke angivet: 1 plaster.\n   Bemærk: 5 timer før virkning ønskes",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                "SimpleNonRepeatedConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 plaster.\n   Bemærk: 5 timer før virkning ønskes",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testOneDayOnly()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("kapsel"),
                    StructureWrapper.MakeStructure(
                        0, "dagen før indlæggelse", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011 og ophører efter det angivne forløb:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. januar 2011: 1 kapsel 2 gange.\n   Bemærk: dagen før indlæggelse",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "LimitedNumberOfDaysConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 kapsel 2 gange.\n   Bemærk: dagen før indlæggelse",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(2, DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value, 0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1StkKl0730FoerIndlaeggelse()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        0, "før indlæggelse", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-01"),
                        DayWrapper.MakeDay(
                            0,
                            TimedDoseWrapper.MakeDose(new LocalTime(7, 30), 1.0, false)))));
            Assert.AreEqual(
                "Doseringen foretages kun lørdag den 1. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   Dag ikke angivet: 1 stk kl. 07:30.\n   Bemærk: før indlæggelse",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleNonRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 stk kl. 07:30.\n   Bemærk: før indlæggelse",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1StkKl0730()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        0, null, DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-01"),
                        DayWrapper.MakeDay(
                            1,
                            TimedDoseWrapper.MakeDose(new LocalTime(7, 30), 1.0, false)))));
            Assert.AreEqual(
                "Doseringen foretages kun lørdag den 1. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   Lørdag den 1. januar 2011: 1 stk kl. 07:30",
                DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual(
                    "SimpleNonRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 stk kl. 07:30",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.OneTime, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }	
}
