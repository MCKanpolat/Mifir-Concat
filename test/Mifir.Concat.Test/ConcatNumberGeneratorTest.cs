using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mifir.Concat.Test
{

    [TestClass]
    public class ConcatNumberGeneratorTest
    {
        private readonly IConcatNumberGenerator _instance;
        public ConcatNumberGeneratorTest()
        {
            _instance = new ConcatNumberGenerator();
        }

        public void ConcatTester(string countryCode, string birthDate, string firstName, string lastName,
            string expected)
        {
            var result = _instance.Generate(new ClientInformation(countryCode: countryCode,
                    dateofBirth: DateTime.ParseExact(birthDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                    firstname: firstName,
                    surname: lastName));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryCodeInvalidException))]
        public void Check_CountryCode_Invalid()
        {
            ConcatTester("NX", "19801224", "Mr Jon", "Anderson", "");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Argument_Exception_FirstName()
        {
            ConcatTester("NO", "19801224", "", "Snow", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Argument_Exception_Surname()
        {
            ConcatTester("NO", "19801224", "John", "", "");
        }

        [TestMethod]
        public void Verify_Expected()
        {
            ConcatTester("NO", "19801224", "Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("GB", "19800122", "Sir Jon", "Snow", "GB19800122JON##SNOW#");
            ConcatTester("US", "19800502", "Dr. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("NO", "19801224", "Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "Jon", "Snow", "NO19801224JON##SNOW#");
        }

        // Verify ESMA Guideline examples
        [TestMethod]
        public void Verify_ESMA_Guideline()
        {
            /* John O'Brian  */
            ConcatTester("IE", "19800113", "John", "O'Brian", "IE19800113JOHN#OBRIA");
            // Ludwig Van der Rohe
            ConcatTester("HU", "19810214", "Ludwig", "Van der Rohe", "HU19810214LUDWIROHE#");
            // Victor Vandenberg US19730322VICTOVANDE
            ConcatTester("US", "19730322", "Victor", "Vandenberg", "US19730322VICTOVANDE");
            // Eli Ødegård
            ConcatTester("NO", "19760315", "Eli", "Ødegård", "NO19760315ELI##ODEGA");
            // Willeke de Bruijn
            ConcatTester("LU", "19660416", "Willeke", "de Bruijn", "LU19660416WILLEBRUIJ");
            // Jon Ian Dewitt
            ConcatTester("US", "19650417", "Jon Ian", "Dewitt", "US19650417JON##DEWIT");
            // Amy-Ally Garção de Magalhães
            ConcatTester("PT", "19900517", "Amy-Ally", "Garção de Magalhães", "PT19900517AMYALGARCA");
            // Giovani dos Santos
            ConcatTester("FR", "19900618", "Giovani", "dos Santos", "FR19900618GIOVASANTO");
            // Günter Voẞ
            ConcatTester("DE", "19800715", "Günter", "Voẞ", "DE19800715GUNTEVOS##");
        }


        // Verify ESMA Guideline examples
        [TestMethod]
        public void Verify_Guideline_Examples()
        {
            // Sean Murphy
            ConcatTester("IE", "19760227", "SEAN", "MURPHY", "IE19760227SEAN#MURPH");
            // Thomas Maccormack
            ConcatTester("IE", "19511212", "THOMAS", "MACCORMACK", "IE19511212THOMAMACCO");
            // Pierre Marie Dupont
            ConcatTester("FR", "19760227", "PIERRE MARIE", "DUPONT", "FR19760227PIERRDUPON");
        }
    }
}
