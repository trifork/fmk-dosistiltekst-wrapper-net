using NUnit.Framework;

using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_test;
using fmk_dosistiltekst_wrapper_net.vowrapper;

namespace fmk_dosistiltekst_wrapper_net.ns20120601 {

    public class CombinedTwoPeriodesConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void testCombined1()
        {
            // 1 dråbe i hvert øje 4 gange 1. dag, derefter 1 dråbe 2 x daglig
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("dråbe", "dråber"),
                    StructureWrapper.MakeStructure(
                        0,
                        null,
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-01"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0))),
                    StructureWrapper.MakeStructure(
                        1,
                        null,
                        DateOrDateTimeWrapper.MakeDate("2011-01-02"), null,
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0),
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "DefaultMultiPeriodeLongTextConverterImpl",
                DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "CombinedTwoPeriodesConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Første dag 1 dråbe 4 gange, herefter 1 dråbe 2 gange daglig",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void testCombined2()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("dråbe", "dråber"),
                    StructureWrapper.MakeStructure(
                        0,
                        null,
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-03"),
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            2,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0)),
                        DayWrapper.MakeDay(
                            3,
                            MorningDoseWrapper.MakeDose(2.0),
                            EveningDoseWrapper.MakeDose(2.0))),
                    StructureWrapper.MakeStructure(
                        1,
                        null,
                        DateOrDateTimeWrapper.MakeDate("2011-01-02"), null,
                        DayWrapper.MakeDay(
                            1,
                            MorningDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "DefaultMultiPeriodeLongTextConverterImpl",
                DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    "CombinedTwoPeriodesConverterImpl",
                    DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.AreEqual(
                    "2 dråber morgen og aften i 3 dage, herefter 1 dråbe morgen og aften",
                    DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.IsNull(DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value);
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }

    }
}
