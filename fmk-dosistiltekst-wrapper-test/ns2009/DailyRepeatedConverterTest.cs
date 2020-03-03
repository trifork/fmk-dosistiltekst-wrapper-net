using System;
using fmk_dosistiltekst_wrapper_net.vowrapper;
using fmk_dosistiltekst_wrapper_net;
using NUnit.Framework;
using fmk_dosistiltekst_wrapper_test;

namespace fmk_dosistiltekst_wrapper_net.ns2009
{
    [TestFixture]
    public class DailyRepeatedConverterTest : AbstractDosageWrapperTest
    {
        [Test]
        public void Test1Stk2til3GangeDgligVedMaaltid()
        {
            var dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        1, "ved måltid",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1),
                            PlainDoseWrapper.MakeDose(1),
                            PlainDoseWrapper.MakeDose(1, true)))));

            Assert.AreEqual(
                    "1 stk 2-3 gange daglig.\nBemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));

            AssertLongTextEquals(dosage);

            Assert.AreEqual("DailyRepeatedConverterImpl", DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual("ParacetamolConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.True(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
