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
    class DosageWrapperTest : AbstractDosageWrapperTest
    {
        [Test]
        public void testDaglig4StkModSmerter2Gange()
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
                                PlainDoseWrapper.MakeDose(4.0)))));

            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            Assert.AreEqual("4 stk 2 gange daglig.\n   Bemærk: mod smerter", shortText);

            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011 og gentages hver dag:\n" +
                "   Doseringsforløb:\n" +
                "   4 stk 2 gange daglig.\n   Bemærk: mod smerter",
                DosisTilTekstWrapper.ConvertLongText(dosage));

            DosageTranslationCombined combined = DosisTilTekstWrapper.ConvertCombined(dosage);
            Assert.AreEqual("4 stk 2 gange daglig.\n   Bemærk: mod smerter", combined.CombinedTranslation.ShortText);
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   4 stk 2 gange daglig.\n   Bemærk: mod smerter", combined.CombinedTranslation.LongText);
            Assert.AreEqual(1, combined.PeriodTranslations.Count);
            Assert.AreEqual("4 stk 2 gange daglig.\n   Bemærk: mod smerter", combined.PeriodTranslations[0].ShortText);
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011 og gentages hver dag:\n" +
                    "   Doseringsforløb:\n" +
                    "   4 stk 2 gange daglig.\n   Bemærk: mod smerter", combined.PeriodTranslations[0].LongText);
        }

        [Test]
        public void testDaglig4StkModSmerterPlus4StkEfterBehovModSmerter()
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
                                    PlainDoseWrapper.MakeDose(4.0, true)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011 og gentages hver dag:\n" +
                "   Doseringsforløb:\n" +
                "   4 stk + 4 stk efter behov.\n   Bemærk: mod smerter",
                DosisTilTekstWrapper.ConvertLongText(dosage));
        }

        [Test]
        public void testHverAndenDagEtc()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                            2, "mod smerter",
                            DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-14"),
                            DayWrapper.MakeDay(1,
                                PlainDoseWrapper.MakeDose(1.0)),
                            DayWrapper.MakeDay(2,
                                PlainDoseWrapper.MakeDose(2.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, forløbet gentages hver 2. dag, og ophører fredag den 14. januar 2011.\n" +
                "Bemærk at doseringen varierer:\n" +
                "   Doseringsforløb:\n" +
                "   Dag 1: 1 stk\n" +
                "   Dag 2: 2 stk.\n   Bemærk: mod smerter",
                DosisTilTekstWrapper.ConvertLongText(dosage));
        }

        [Test]
        public void testMorgenMiddagAftenNat()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                        1, "mod smerter",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-14"),
                        DayWrapper.MakeDay(1,
                            MorningDoseWrapper.MakeDose(1.0),
                            NoonDoseWrapper.MakeDose(1.0),
                            EveningDoseWrapper.MakeDose(1.0),
                            NightDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører fredag den 14. januar 2011:\n" +
                "   Doseringsforløb:\n" +
                "   1 stk morgen + 1 stk middag + 1 stk aften + 1 stk nat.\n   Bemærk: mod smerter",
                DosisTilTekstWrapper.ConvertLongText(dosage));
        }

        [Test]
        public void testMaxQuantityFullCiphers()
        {

            var minimalQuantity = 0.000000001;
            var maximalQuantity = 999999999.99999;
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(
                    StructuresWrapper.MakeStructures(
                        UnitOrUnitsWrapper.MakeUnit("stk"),
                        StructureWrapper.MakeStructure(
                        1, "mod smerter",
                        DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-14"),
                        DayWrapper.MakeDay(1,
                            MorningDoseWrapper.MakeDose(minimalQuantity, maximalQuantity)))));

            DailyDosis dd = DosisTilTekstWrapper.CalculateDailyDosis(dosage);
            Assert.AreEqual(minimalQuantity, dd.Interval.MinimumValue);
            Assert.AreEqual(maximalQuantity, dd.Interval.MaximumValue);
        }

        [Test]
        public void testCombinedWithFreeText()
        {
            DosageWrapper dosage =
                DosageWrapper.MakeDosage(FreeTextWrapper.MakeFreeText(DateOrDateTimeWrapper.MakeDate("2011-01-01"), null, "Bare tag rigeligt"));

            DosageTranslationCombined combined = DosisTilTekstWrapper.ConvertCombined(dosage);
            Assert.AreEqual("Bare tag rigeligt", combined.CombinedTranslation.ShortText);
            Assert.AreEqual(
                    "Doseringsforløbet starter lørdag den 1. januar 2011.\n" +
                    "   Doseringsforløb:\n" +
                    "   Bare tag rigeligt", combined.CombinedTranslation.LongText);
        }

        [Test]
        public void testREADMEExampleCode()
        {

       
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter"), 
                    StructureWrapper.MakeStructure(1, "ved måltid", DateOrDateTimeWrapper.MakeDate("2011-01-01"), DateOrDateTimeWrapper.MakeDate("2011-01-30"), 
                        DayWrapper.MakeDay(1, PlainDoseWrapper.MakeDose(1.0), 
                            PlainDoseWrapper.MakeDose(1.0), 
                            PlainDoseWrapper.MakeDose(1.0, true)
                        )
                    )
                )
            );
  
            string longText = DosisTilTekstWrapper.ConvertLongText(dosage);
            string shortText = DosisTilTekstWrapper.ConvertShortText(dosage);
            DailyDosis daily = DosisTilTekstWrapper.CalculateDailyDosis(dosage);
            DosageType dosageType = DosisTilTekstWrapper.GetDosageType(dosage);
            DosageTranslationCombined combined = DosisTilTekstWrapper.ConvertCombined(dosage);
        }

        [Test]
        public void testAlDenteTestCase()
        {
            DosageWrapper DosWrapper;

            UnitOrUnitsWrapper Enhed;
            DayWrapper[] Dage;
            DoseWrapper[] Doser;
            StructureWrapper[] Strukturer;
            StructuresWrapper DosisStrukturer;

            DosageTranslationCombined Combined;

            Enhed = UnitOrUnitsWrapper.MakeUnits("tablet", "tabletter");

            Dage = new DayWrapper[2];
            Doser = new DoseWrapper[1];
            Doser[0] = MorningDoseWrapper.MakeDose(2, false);
            Dage[0] = DayWrapper.MakeDay(1, Doser);

            Doser = new DoseWrapper[1];
            Doser[0] = NoonDoseWrapper.MakeDose(1, 3, false);
            Dage[1] = DayWrapper.MakeDay(2, Doser);

            Strukturer = new StructureWrapper[1];
            Strukturer[0] = StructureWrapper.MakeStructure(2,
                                                           "tages med rigeligt væske",
                                                           DateOrDateTimeWrapper.MakeDate(DateTime.Today),
                                                           DateOrDateTimeWrapper.MakeDate(DateTime.Today.AddDays(10)),
                                                           Dage);

            DosisStrukturer = StructuresWrapper.MakeStructures(Enhed, Strukturer);
            DosWrapper = DosageWrapper.MakeDosage(DosisStrukturer);

            Console.WriteLine(DosisTilTekstWrapper.ConvertLongText(DosWrapper)); // OK

            Combined = DosisTilTekstWrapper.ConvertCombined(DosWrapper); // FEJLER
            Assert.IsNotNull(Combined);
            Assert.IsNull(Combined.CombinedTranslation.ShortText);
            Assert.IsNotNull(Combined.CombinedTranslation.LongText);
            Assert.IsNotNull(Combined.CombinedTranslation.DailyDosis);
        }

         [Test]
        public void testShortTextToLong()
        {
            DosageWrapper dosage = DosageWrapper.MakeDosage(
                StructuresWrapper.MakeStructures(
                    UnitOrUnitsWrapper.MakeUnit("stk"),
                    StructureWrapper.MakeStructure(
                        7, "en meget, meget, meget laaaang supplerende tekst", DateOrDateTimeWrapper.MakeDate("2012-06-08"), DateOrDateTimeWrapper.MakeDate("2012-12-31"),
                        DayWrapper.MakeDay(
                            1,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            3,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            5,
                            PlainDoseWrapper.MakeDose(1.0)),
                        DayWrapper.MakeDay(
                            7,
                            PlainDoseWrapper.MakeDose(1.0)))));
            Assert.AreEqual(
                    "WeeklyRepeatedConverterImpl",
                    DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual(
                    "Doseringsforløbet starter fredag den 8. juni 2012, forløbet gentages hver uge, og ophører mandag den 31. december 2012.\n" +
                    "Bemærk at doseringen har et komplekst forløb:\n" +
                    "   Doseringsforløb:\n" +
                    "   Tirsdag: 1 stk\n" +
                    "   Torsdag: 1 stk\n" +
                    "   Fredag: 1 stk\n" +
                    "   Søndag: 1 stk.\n   Bemærk: en meget, meget, meget laaaang supplerende tekst",
                    DosisTilTekstWrapper.ConvertLongText(dosage));
            Assert.AreEqual("1 stk tirsdag, torsdag, fredag og søndag hver uge.\n   Bemærk: en meget, meget, meget laaaang supplerende tekst", DosisTilTekstWrapper.ConvertShortText(dosage, 300));
            Assert.IsNull(DosisTilTekstWrapper.ConvertShortText(dosage));
        }
    }
}
