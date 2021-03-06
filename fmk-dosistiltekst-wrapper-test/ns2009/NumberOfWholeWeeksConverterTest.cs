using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_net.vowrapper;
using fmk_dosistiltekst_wrapper_test;
using NUnit.Framework;

namespace fmk_dosistiltekst_wrapper_net.ns2009
{

    public class NumberOfWholeWeeksConverterTest : AbstractDosageWrapperTest
    {

        [Test]
        public void test1stkMorgen1Uge1UgesPause()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    14, "ved måltid", DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.AreEqual(
                    0.5,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgenOgAften1Uge1UgesPause_IngenKortTekst()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    14, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, NoonDoseWrapper.MakeDose(1.0), EveningDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stk2GangeDaglig1Uge1UgesPause()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    14, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, PlainDoseWrapper.MakeDose(1.0), PlainDoseWrapper.MakeDose(1.0))
                            )
                    ));
            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 stk 2 gange daglig i en uge, herefter en uges pause", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgen1Uge5UgersPause()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 stk middag daglig i en uge, herefter 5 ugers pause", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.166666667,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgen2Uge4UgersPause()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(8, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(9, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(10, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(11, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(12, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(13, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(14, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 stk middag daglig i 2 uger, herefter 4 ugers pause", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.333333333,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgen2UgeIngenPause()
        {
            // This is really abuse og a bug structure
            // Equivalent with one day and iteration=1.
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    14, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(7, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(8, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(9, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(10, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(11, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(12, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(13, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(14, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.AreEqual("1 stk middag daglig", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    1.0,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgen3dageHver6uger()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));
        
                AssertLongTextEquals(dosage);
            Assert.AreEqual("1 stk middag daglig i 3 dage, herefter 39 dages pause", DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.071428571,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgenDag135Hver6uger_IngenKortTekst()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(1, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(3, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(5, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.071428571,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }

        [Test]
        public void test1stkMorgenDag246Hver6uger_IngenKortTekst()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                    42, null, DateOrDateTimeWrapper.MakeDateTime("2014-02-07 07:19:00"), null,
                                    DayWrapper.MakeDay(2, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(4, NoonDoseWrapper.MakeDose(1.0)),
                                    DayWrapper.MakeDay(6, NoonDoseWrapper.MakeDose(1.0))
                            )
                    ));

            AssertLongTextEquals(dosage);
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
            Assert.AreEqual(
                    0.071428571,
                    DosisTilTekstWrapper.CalculateDailyDosis(dosage).Value.Value,
                    0.000000001);
            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
