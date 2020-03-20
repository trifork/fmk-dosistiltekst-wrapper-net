using NUnit.Framework;
using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

namespace fmk_dosistiltekst_wrapper_net.ns20120601
{
    public class DayInWeekConverterTest : AbstractDosageWrapperTest
    {

        // FMK-3273
        [Test]
        public void testSupplText()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"),
                    StructureWrapper.MakeStructure(
                        14,
                        "ved måltid",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            3, MorningDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            10, MorningDoseWrapper.MakeDose(1.0)))
                    ));

            Assert.AreEqual(
                    "DayInWeekConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "1 tablet morgen daglig mandag.\nBemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));

            Assert.AreEqual(
                    "DefaultLongTextConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
        }
    }
}
