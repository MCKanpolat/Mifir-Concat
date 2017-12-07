using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

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
            ConcatTester("NO", "19801224", "1Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "!Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "Jon", "?Snow", "NO19801224JON##SNOW#");
            ConcatTester("GB", "19800122", "Sir Jon", "Snow", "GB19800122JON##SNOW#");
            ConcatTester("NO", "19801224", "Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("US", "19800502", "Dr. Robert", "Ford", "US19800502ROBERFORD#");
        }

        [TestMethod]
        public void Verify_Remove_Prefix()
        {
            ConcatTester("NO", "19801224", "DE L’ Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "de l’ Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "AUF DEM Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "auf dem Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DE LA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "de le Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DE LE Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "de le Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MHIC GIOLLA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mhic GIOLLA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VAN DE micheal", "jackson", "NO19801224MICHEJACKS");
            ConcatTester("NO", "19801224", "van de Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VAN DEN Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "van den Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VAN DER Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "van der Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VON DEM micheal", "jackson", "NO19801224MICHEJACKS");
            ConcatTester("NO", "19801224", "von dem Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VON DER Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "von der micheal", "Snow", "NO19801224MICHESNOW#");

            ConcatTester("NO", "19801224", "D Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DE Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DEL Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DI Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DO Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DOS Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DU Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "IM Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "LA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "LE Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MAC Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MC Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MHAC Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MHÍC Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "MIC micheal", "jackson", "NO19801224MICHEJACKS");
            ConcatTester("NO", "19801224", "NI Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "NÍ Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "NÍC Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "O Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "Ó Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "UA Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "UI Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "UÍ Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VAN Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VOM Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "VON Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "DEN Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "d Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "da Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "de Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "del Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "di Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "do Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "dos Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "du Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "im Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "la Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "le Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mac Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mc Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mhac Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mhíc Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "mic micheal", "jackson", "NO19801224MICHEJACKS");
            ConcatTester("NO", "19801224", "ni Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "ní Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "níc Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "o Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "ó Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "ua Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "ui Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "uí Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "van Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "vom Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "von Jon", "Snow", "NO19801224JON##SNOW#");
            ConcatTester("NO", "19801224", "den Jon", "Snow", "NO19801224JON##SNOW#");
        }

        [TestMethod]
        public void Verify_Remove_Titles()
        {
            ConcatTester("US", "19800502", "ATTY Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "COACH Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "DAME Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "DR. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "FR. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "GOV Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "HONORABLE Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MADAM Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MADAME Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MAID Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MASTER Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MISS Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MONSIEUR Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MR. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MRS. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MS Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "MX Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "OFC Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "PH.D Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "PRES Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "PROF Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "REV Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "SIR Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "atty Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "coach Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "dame Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "dr. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "fr. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "gov. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "honorable Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "madam Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "madame Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "maid Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "master Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "miss Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "monsieur Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "mr. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "mrs. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "ms Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "mx Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "ofc Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "ph.d Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "pres Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "prof. Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "rev Robert", "Ford", "US19800502ROBERFORD#");
            ConcatTester("US", "19800502", "sir Robert", "Ford", "US19800502ROBERFORD#");
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
