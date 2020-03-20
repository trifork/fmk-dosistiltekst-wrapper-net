using NUnit.Framework;

using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

namespace fmk_dosistiltekst_wrapper_net.ns20120601
{
 
    public class RepeatedEyeOrEarConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testOneInEachEyeTwice()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("dråbe", "dråber"),
                    StructureWrapper.MakeStructure(
                        1, "1 i hvert øje", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(2.0),
                            PlainDoseWrapper.MakeDose(2.0)))));
            Assert.AreEqual(
                    "DailyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                "RepeatedEyeOrEarConverterImpl",
                DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                "1 dråbe 2 gange daglig.\nBemærk: i begge øjne",
                DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    4.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
