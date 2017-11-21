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
    class DosageTypeCalculatorTest : AbstractDosageWrapperTest
    {
        [Test]
        public void testTemporary()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-02-01"),
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Temporary, DosisTilTekstWrapper.GetDosageType(dosage));

        }

        [Test]
        public void testMultipleStructuresAllFixed()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0))),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Fixed, DosisTilTekstWrapper.GetDosageType(dosage));

        }

        [Test]
        public void testMultipleStructuresAllPN()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0, true),
                                    PlainDoseWrapper.MakeDose(4.0, true))),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0, true),
                                    PlainDoseWrapper.MakeDose(4.0, true)))
                        ));

            Assert.AreEqual(DosageType.AccordingToNeed, DosisTilTekstWrapper.GetDosageType(dosage));

        }

        [Test]
        public void testMultipleStructuresCombined()
        {
            DosageWrapper dosage =
                    DosageWrapper.MakeDosage(
                        StructuresWrapper.MakeStructures(
                            UnitOrUnitsWrapper.MakeUnit("stk"),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0),
                                    PlainDoseWrapper.MakeDose(4.0, true))),
                            StructureWrapper.MakeStructure(
                                1, "mod smerter",
                                DateOrDateTimeWrapper.MakeDate("2011-01-01"), null,
                                DayWrapper.MakeDay(1,
                                    PlainDoseWrapper.MakeDose(4.0, true),
                                    PlainDoseWrapper.MakeDose(4.0)))
                        ));

            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));

        }
    }
}
