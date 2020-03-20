using fmk_dosistiltekst_wrapper_net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_test
{
    public class DosageProposalXMLGeneratorTest : AbstractDosageWrapperTest
    {
        [Test]
        public void testBasic()
        {
            DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("PN", "1", "1", "tablet", "tabletter", "tages med rigeligt vand", new [] { new DateTime(2017,5,17) }, new [] { new DateTime?(new DateTime(2017,6,1)) }, FMKVersion.FMK146, 1);
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.LongText);
            Assert.AreEqual("Dosering fra d. 17. maj 2017 til d. 1. juni 2017:\n" +
                    "1 tablet efter behov højst 1 gang dagligt\nBemærk: tages med rigeligt vand", res.LongText);
            Assert.AreEqual("<m16:Dosage xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01 ../../../2015/06/01/DosageForRequest.xsd\" xmlns:m16=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><m16:UnitTexts source=\"Doseringsforslag\"><m16:Singular>tablet</m16:Singular><m16:Plural>tabletter</m16:Plural></m16:UnitTexts><m16:StructuresAccordingToNeed><m16:Structure><m16:IterationInterval>1</m16:IterationInterval><m16:StartDate>2017-05-17</m16:StartDate><m16:EndDate>2017-06-01</m16:EndDate><m16:SupplementaryText>tages med rigeligt vand</m16:SupplementaryText><m16:Day><m16:Number>1</m16:Number><m16:Dose><m16:Quantity>1</m16:Quantity></m16:Dose></m16:Day></m16:Structure></m16:StructuresAccordingToNeed></m16:Dosage>", res.XmlSnippet);
        }

         [Test]
        public void testBasic2()
        {
            DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("PN", "1", "1;1;1", "plaster", "plastre", "", new [] { new DateTime(2017,5,17) }, new [] { new DateTime?(new DateTime(2017,6,1)) }, FMKVersion.FMK146, 1);
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.LongText);
            Assert.AreEqual("Dosering fra d. 17. maj 2017 til d. 1. juni 2017:\n" +
                    "1 plaster efter behov højst 3 gange dagligt", res.LongText);
            Assert.AreEqual("<m16:Dosage xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01 ../../../2015/06/01/DosageForRequest.xsd\" xmlns:m16=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><m16:UnitTexts source=\"Doseringsforslag\"><m16:Singular>plaster</m16:Singular><m16:Plural>plastre</m16:Plural></m16:UnitTexts><m16:StructuresAccordingToNeed>" + 
                "<m16:Structure><m16:IterationInterval>1</m16:IterationInterval>" +
                "<m16:StartDate>2017-05-17</m16:StartDate><m16:EndDate>2017-06-01</m16:EndDate>" + 
                "<m16:Day><m16:Number>1</m16:Number>" + 
                "<m16:Dose><m16:Quantity>1</m16:Quantity></m16:Dose>" + 
                "<m16:Dose><m16:Quantity>1</m16:Quantity></m16:Dose>" + 
                "<m16:Dose><m16:Quantity>1</m16:Quantity></m16:Dose>" + 
                "</m16:Day>" +
                "</m16:Structure></m16:StructuresAccordingToNeed></m16:Dosage>", res.XmlSnippet);
        }


         [Test]
        public void testBasicWithLongerShortText()
        {
            DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("PN", "1", "1", "tablet", "tabletter", "tages med rigeligt vand OG NOGET MERE SOM GØR DEN KORTE DOSERINGSTEKST NOGET LÆNGERE END DE SÆDVANLIGE 70 KARAKTERER", new [] { new DateTime(2017,5,17) }, new [] { new DateTime?(new DateTime(2017,6,1)) }, FMKVersion.FMK146, 1, 10000);
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.LongText);
            Assert.AreEqual("Dosering fra d. 17. maj 2017 til d. 1. juni 2017:\n" +
                    "1 tablet efter behov højst 1 gang dagligt\nBemærk: tages med rigeligt vand OG NOGET MERE SOM GØR DEN KORTE DOSERINGSTEKST NOGET LÆNGERE END DE SÆDVANLIGE 70 KARAKTERER", res.LongText);
            Assert.AreEqual("1 tablet efter behov, højst 1 gang dagligt.\nBemærk: tages med rigeligt vand OG NOGET MERE SOM GØR DEN KORTE DOSERINGSTEKST NOGET LÆNGERE END DE SÆDVANLIGE 70 KARAKTERER", res.ShortText);
            Assert.AreEqual("<m16:Dosage xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01 ../../../2015/06/01/DosageForRequest.xsd\" xmlns:m16=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><m16:UnitTexts source=\"Doseringsforslag\"><m16:Singular>tablet</m16:Singular><m16:Plural>tabletter</m16:Plural></m16:UnitTexts><m16:StructuresAccordingToNeed><m16:Structure><m16:IterationInterval>1</m16:IterationInterval><m16:StartDate>2017-05-17</m16:StartDate><m16:EndDate>2017-06-01</m16:EndDate><m16:SupplementaryText>tages med rigeligt vand OG NOGET MERE SOM GØR DEN KORTE DOSERINGSTEKST NOGET LÆNGERE END DE SÆDVANLIGE 70 KARAKTERER</m16:SupplementaryText><m16:Day><m16:Number>1</m16:Number><m16:Dose><m16:Quantity>1</m16:Quantity></m16:Dose></m16:Day></m16:Structure></m16:StructuresAccordingToNeed></m16:Dosage>", res.XmlSnippet);
        }


// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) daglig(t)+                                         "N daglig","1", "<quantity>","<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) morgen                                              "M+M+A+N","1", "<quantity>","<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) 2 gange daglig                 "N daglig","1", "<quantity>;<quantity>", "<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) 3 gange daglig                 "N daglig","1", "<quantity>;<quantity>;<quantity>", "<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) til natten                     "M+M+A+N","1", "0+0+0+<quantity>","<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) morgen og aften                "M+M+A+N","1", "<quantity>+0+<quantity>+0","<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) aften                           "M+M+A+N","1", "0+0+<quantity>+0","<quantityunit>","<quantityunit>"
// (?<quantity>[0-9,.]+) (?<quantityunit>[A-Za-z]+) (?<iteration>[0-9]+) gange i (?<days>[0-9]+) dage                           "M+M+A+N","1", "0+0+<quantity>+0","<quantityunit>","<quantityunit>"
// 

/*
  2278 | 1 tablet ved behov                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
|  2317 | efter skema                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
|  2430 | 1 kapsel daglig                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|  2591 | 2 tabletter ved behov. Max 8 tabletter per dag.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|  2817 | 1 tablet 3 gange daglig i 5 dage                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
|  2992 | 1 tablet 3 gange daglig i 7 dage                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
|  3035 | Dosering efter skriftlig anvisning                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
|  3101 | efter behov                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
|  3128 | 1 tablet aften                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
|  3309 | 1 tablet ved behov. Max 3 tablet per dag.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
|  3510 | 1 tablet morgen og aften                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
|  3930 | ingen doserings tekst                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
|  3936 | 1 tablet til natten                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
|  4076 | 1 tablet 2 gange daglig                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
|  5146 | 1 tablet 3 gange daglig                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
|  5612 | til injektion hos lægen                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
|  6198 | 1 tablet dagligt                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
|  8621 | 1 tablet morgen                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| 17520 | Efter aftale                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| 20731 | 1 tablet daglig                                                                     
*/
        [Test]
        public void testMultiPeriode()
        {

            DosageProposalResult res = DosisTilTekstWrapper.GetDosageProposalResult("{M+M+A+N}{PN}{N daglig}", "{1}{2}{1}",
                    "{1+2+3+4}{dag 1: 2 dag 2: 3}{2}", "tablet", "tabletter", "tages med rigeligt vand",
                    new[] { new DateTime(2010, 1, 1), new DateTime(2010, 2, 1), new DateTime(2010, 3, 1) },
                    new[] { new DateTime?(new DateTime(2010, 1, 31)), new DateTime?(new DateTime(2010, 2, 28)), new DateTime?(new DateTime(2010, 3, 31)) },
                    FMKVersion.FMK146, 1);
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.LongText);
            Assert.AreEqual("<m16:Dosage xsi:schemaLocation=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01 ../../../2015/06/01/DosageForRequest.xsd\" xmlns:m16=\"http://www.dkma.dk/medicinecard/xml.schema/2015/06/01\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                    "<m16:UnitTexts source=\"Doseringsforslag\">" +
                    "<m16:Singular>tablet</m16:Singular>" +
                    "<m16:Plural>tabletter</m16:Plural>" +
                    "</m16:UnitTexts>" +
                    "<m16:StructuresFixed>" +
                    "<m16:Structure>" +
                    "<m16:IterationInterval>1</m16:IterationInterval>" +
                    "<m16:StartDate>2010-01-01</m16:StartDate>" +
                    "<m16:EndDate>2010-01-31</m16:EndDate>" +
                    "<m16:SupplementaryText>tages med rigeligt vand</m16:SupplementaryText>" +
                    "<m16:Day>" +
                    "<m16:Number>1</m16:Number>" +
                    "<m16:Dose>" +
                    "<m16:Time>morning</m16:Time>" +
                    "<m16:Quantity>1</m16:Quantity>" +
                    "</m16:Dose>" +
                    "<m16:Dose>" +
                    "<m16:Time>noon</m16:Time>" +
                    "<m16:Quantity>2</m16:Quantity>" +
                    "</m16:Dose>" +
                    "<m16:Dose>" +
                    "<m16:Time>evening</m16:Time>" +
                    "<m16:Quantity>3</m16:Quantity>" +
                    "</m16:Dose>" +
                    "<m16:Dose>" +
                    "<m16:Time>night</m16:Time>" +
                    "<m16:Quantity>4</m16:Quantity>" +
                    "</m16:Dose>" +
                    "</m16:Day>" +
                    "</m16:Structure>" +
                    "<m16:Structure>" +
                    "<m16:IterationInterval>1</m16:IterationInterval>" +
                    "<m16:StartDate>2010-03-01</m16:StartDate>" +
                    "<m16:EndDate>2010-03-31</m16:EndDate>" +
                    "<m16:SupplementaryText>tages med rigeligt vand</m16:SupplementaryText>" +
                    "<m16:Day>" +
                    "<m16:Number>1</m16:Number>" +
                    "<m16:Dose><m16:Quantity>2</m16:Quantity></m16:Dose>" +
                    "</m16:Day>" +
                    "</m16:Structure>" +
                    "</m16:StructuresFixed>" +
                    "<m16:StructuresAccordingToNeed>" +
                    "<m16:Structure>" +
                    "<m16:IterationInterval>2</m16:IterationInterval>" +
                    "<m16:StartDate>2010-02-01</m16:StartDate>" +
                    "<m16:EndDate>2010-02-28</m16:EndDate>" +
                    "<m16:SupplementaryText>tages med rigeligt vand</m16:SupplementaryText>" +
                    "<m16:Day>" +
                    "<m16:Number>1</m16:Number>" +
                    "<m16:Dose><m16:Quantity>2</m16:Quantity></m16:Dose>" +
                    "</m16:Day>" +
                    "<m16:Day>" +
                    "<m16:Number>2</m16:Number>" +
                    "<m16:Dose><m16:Quantity>3</m16:Quantity></m16:Dose>" +
                    "</m16:Day>" +
                    "</m16:Structure>" +
                    "</m16:StructuresAccordingToNeed>" +
                    "</m16:Dosage>", res.XmlSnippet);

            Assert.AreEqual("Dosering fra d. 1. jan. 2010 til d. 31. jan. 2010:\n" +
            "1 tablet morgen, 2 tabletter middag, 3 tabletter aften og 4 tabletter nat - hver dag\nBemærk: tages med rigeligt vand\n\n" +
            "Dosering fra d. 1. mar. 2010 til d. 31. mar. 2010:\n" +
            "2 tabletter hver dag\nBemærk: tages med rigeligt vand\n\n" +
            "Dosering som gentages hver 2. dag fra d. 1. feb. 2010 til d. 28. feb. 2010:\n" +
            "Dag 1: 2 tabletter efter behov højst 1 gang dagligt\n" +
            "Dag 2: 3 tabletter efter behov højst 1 gang dagligt\nBemærk: tages med rigeligt vand"
           , res.LongText);
        }
    }
}
