﻿using System;
using fmk_dosistiltekst_wrapper_net.vowrapper;
using fmk_dosistiltekst_wrapper_net;
using NUnit.Framework;

namespace fmk_dosistiltekst_wrapper_test.ns2009
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
                    "1 stk 2-3 gange daglig.\n   Bemærk: ved måltid",
                    DosisTilTekstWrapper.ConvertShortText(dosage));

            Assert.AreEqual(
              "Doseringsforløbet starter lørdag den 1. januar 2011, gentages hver dag, og ophører søndag den 30. januar 2011:\n" +
              "   Doseringsforløb:\n" +
              "   1 stk + 1 stk + 1 stk efter behov.\n   Bemærk: ved måltid",
              DosisTilTekstWrapper.ConvertLongText(dosage));

            Assert.AreEqual("DailyRepeatedConverterImpl", DosisTilTekstWrapper.GetLongTextConverterClassName(dosage));
            Assert.AreEqual("ParacetamolConverterImpl", DosisTilTekstWrapper.GetShortTextConverterClassName(dosage));
            Assert.True(DosisTilTekstWrapper.CalculateDailyDosis(dosage).IsNone());
            Assert.AreEqual(DosageType.Combined, DosisTilTekstWrapper.GetDosageType(dosage));
        }
    }
}
