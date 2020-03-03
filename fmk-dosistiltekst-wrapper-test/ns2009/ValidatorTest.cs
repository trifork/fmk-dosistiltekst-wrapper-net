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

    public class ValidatorTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testJiraFMK903()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                    1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:00"), null,
                    DayWrapper.MakeDay(
                        1,
                        MorningDoseWrapper.MakeDose(2.0),
                        EveningDoseWrapper.MakeDose(0.0)))));
            var structureEnum = dosage.Structures.Structures.GetEnumerator();
            structureEnum.MoveNext();
            StructureWrapper s = structureEnum.Current;
            Assert.AreEqual(
                    1,
                    s.Days[0].NumberOfDoses);
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "MorningNoonEveningNightConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk morgen",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    2.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
        }

        [Test]
        public void testJiraFMK903Variant()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, null, DateOrDateTimeWrapper.MakeDateTime("2012-04-13 20:06:00"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.0, true),
                            PlainDoseWrapper.MakeDose(0.0, true)))));
            var structureEnum = dosage.Structures.Structures.GetEnumerator();
            structureEnum.MoveNext();
            StructureWrapper s = structureEnum.Current;
            Assert.AreEqual(1, s.Days[0].AccordingToNeedDoses.Count);
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "SimpleLimitedAccordingToNeedConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 stk efter behov, højst 1 gang daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
        }

    }
}