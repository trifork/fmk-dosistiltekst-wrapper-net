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

    public class LongTextConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testAdministrationAccordingToSchemeInLocalSystem()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                AdministrationAccordingToSchemaWrapper.makeAdministrationAccordingToSchema(null, null));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("Dosering efter skriftlig anvisning", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));

        }

        [Test]
        public void testFreeText()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                FreeTextWrapper.MakeFreeText(null, null, "Dosages in free text should always be avoided"));
            AssertLongTextEquals(dosage);
            Assert.IsTrue(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Unspecified, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNs2009DosageTimes()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("ml"),
                        StructureWrapper.MakeStructure(
                            1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                            DayWrapper.MakeDay(
                                1,
                                MorningDoseWrapper.MakeDose(1.0),
                                EveningDoseWrapper.MakeDose(2.0)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    3.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNs2009Order()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("ml"),
                        StructureWrapper.MakeStructure(
                            0, "før behandling", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                            DayWrapper.MakeDay(
                                1,
                                TimedDoseWrapper.MakeDose(new LocalTime(13, 30, 0), 1.0, false)))));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.OneTime, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testNs2009Order2()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("ml"),
                        StructureWrapper.MakeStructure(
                            0, "før behandling", DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                            DayWrapper.MakeDay(
                                1,
                                TimedDoseWrapper.MakeDose(new LocalTime(13, 30, 0), 1.0, false),
                                TimedDoseWrapper.MakeDose(new LocalTime(14, 30, 0), 2.0, false)))));

            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    3.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
