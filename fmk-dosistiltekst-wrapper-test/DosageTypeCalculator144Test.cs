using fmk_dosistiltekst_wrapper_net;
using fmk_dosistiltekst_wrapper_net.vowrapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_test
{
    public class DosageTypeCalculator144Test : AbstractDosageWrapperTest
    {
        [Test]
        public void testTemporaryBefore144NowFixed()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-02-01"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType144(dosage));
        }

        [Test]
        public void testCombined()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-02-01"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0, true)))
                        ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));
        }

        [Test]
        public void testFixedOnlyReturnsFixed()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-02-01"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType144(dosage));
        }


        [Test]
        public void testMultipleStructuresAllFixedReturnsFixed()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-01-03"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0))),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-04"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType144(dosage));

        }

        [Test]
        public void testMultipleStructuresAllPNReturnsPN()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-01-03"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0, true),
                                    PlainDoseWrapper.MakeDose(4.0, true))),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-04"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0, true),
                                    PlainDoseWrapper.MakeDose(4.0, true)))
                        ));

            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType144(dosage));

        }

        [Test]
        public void testMultipleStructuresInternallyCombinedReturnsCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0),
                                PlainDoseWrapper.MakeDose(4.0, true))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0, true),
                                PlainDoseWrapper.MakeDose(4.0)))
                    ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));

        }

        [Test]
        public void testNotOverlappingFixedAndEmptyReturnsFixed()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-01"), DateOrDateTimeWrapper.MakeDate("2017-02-02"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0),
                                PlainDoseWrapper.MakeDose(4.0, false))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-03"), DateOrDateTimeWrapper.MakeDate("2017-02-04"))
                    ));

            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType144(dosage));
        }


        [Test]
        public void testOverlappingFixedAndEmptyReturnsCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-01"), DateOrDateTimeWrapper.MakeDate("2017-02-02"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0),
                                PlainDoseWrapper.MakeDose(4.0, false))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-01"), DateOrDateTimeWrapper.MakeDate("2017-02-02"))
                    ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));
        }

        [Test]
        public void testOverlappingFixedWithoutEnddateAndEmptyWithoutEnddateReturnsCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0),
                                PlainDoseWrapper.MakeDose(4.0, false))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null)
                    ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));
        }



        [Test]
        public void testNotOverlappingPNAndEmptyReturnsPN()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-01"), DateOrDateTimeWrapper.MakeDate("2017-02-02"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0, true))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-02-03"), DateOrDateTimeWrapper.MakeDate("2017-02-04"))
                    ));

            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType144(dosage));
        }

        [Test]
        public void testOverlappingPNAndEmptyReturnsCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null,
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(4.0, true))),
                        StructureWrapper.MakeStructure(
                            1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), null)
                    ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));
        }

        // FMK-3247 Forkerte doseringstyper ved tomme doseringsperioder
        [Test]
        public void testWithSeveralEmptyPeriodsReturnsCombined()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-01-03"),
                            DayWrapper.MakeDay(1, PlainDoseWrapper.MakeDose(4.0))),
                        StructureWrapper.MakeStructure(1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-04"), DateOrDateTimeWrapper.MakeDate("2017-01-12")),
                        StructureWrapper.MakeStructure(1, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2017-01-13"), DateOrDateTimeWrapper.MakeDate("2017-01-15"),
                            DayWrapper.MakeDay(1, PlainDoseWrapper.MakeDose(4.0))),
                        StructureWrapper.MakeStructure(1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2017-01-01"), DateOrDateTimeWrapper.MakeDate("2017-01-15"))
                    ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType144(dosage));
        }


    }
}
