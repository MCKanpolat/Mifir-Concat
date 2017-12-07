using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mifir.Concat
{
    public class ConcatNumberGenerator : IConcatNumberGenerator
    {

        #region Fields

        private static readonly string[] _prefix2 = { "DE L’", "AUF DEM", "AUS DER", "DE LA", "DE LE",
                                               "MHIC GIOLLA", "VAN DE", "VAN DEN", "VAN DER", "VON DEM" , "VON DER" };

        private static readonly string[] _prefix =
        {
            "AM", "AUF", "D", "DA", "DE", "DEL", "DI", "DO", "DOS", "DU", "IM", "LA",
            "LE", "MAC", "MC", "MHAC", "MHÍC", "MIC", "NI", "NÍ", "NÍC",
            "O", "Ó", "UA", "UI", "UÍ", "VAN", "VOM", "VON", "DEN"
        };

        private static readonly string[] _ccalpha2 =
        {
            "AD", "AE", "AF", "AG", "AI", "AL", "AM", "AO", "AQ", "AR",
            "AS", "AT", "AU", "AW", "AX", "AZ", "BA", "BB", "BD", "BE", "BF", "BG", "BH",
            "BI", "BJ", "BL", "BM", "BN", "BO", "BQ", "BQ", "BR", "BS", "BT", "BV", "BW",
            "BY", "BZ", "CA", "CC", "CD", "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN",
            "CO", "CR", "CU", "CV", "CW", "CX", "CY", "CZ", "DE", "DJ", "DK", "DM", "DO",
            "DZ", "EC", "EE", "EG", "EH", "ER", "ES", "ET", "FI", "FJ", "FK", "FM", "FO",
            "FR", "GA", "GB", "GD", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GP",
            "GQ", "GR", "GS", "GT", "GU", "GW", "GY", "HK", "HM", "HN", "HR", "HT", "HU",
            "ID", "IE", "IL", "IM", "IN", "IO", "IQ", "IR", "IS", "IT", "JE", "JM", "JO",
            "JP", "KE", "KG", "KH", "KI", "KM", "KN", "KP", "KR", "KW", "KY", "KZ", "LA",
            "LB", "LC", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY", "MA", "MC", "MD",
            "ME", "MF", "MG", "MH", "MK", "ML", "MM", "MN", "MO", "MP", "MQ", "MR", "MS",
            "MT", "MU", "MV", "MW", "MX", "MY", "MZ", "NA", "NC", "NE", "NF", "NG", "NI",
            "NL", "NO", "NP", "NR", "NU", "NZ", "OM", "PA", "PE", "PF", "PG", "PH", "PK",
            "PL", "PM", "PN", "PR", "PS", "PT", "PW", "PY", "QA", "RE", "RO", "RS", "RU",
            "RW", "SA", "SB", "SC", "SD", "SE", "SG", "SH", "SI", "SJ", "SK", "SL", "SM",
            "SN", "SO", "SR", "SS", "ST", "SV", "SX", "SY", "SZ", "TC", "TD", "TF", "TG",
            "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TR", "TT", "TV", "TW", "TZ", "UA",
            "UG", "UM", "US", "UY", "UZ", "VA", "VC", "VE", "VG", "VI", "VN", "VU", "WF",
            "WS", "YE", "YT", "ZA", "ZM", "ZW"
        };

        private static readonly string[] _titles =
        {
            "ATTY", "COACH", "DAME", "DR", "FR", "GOV", "HONORABLE",
            "MADAM", "MADAME", "MAID", "MASTER", "MISS", "MONSIEUR", "MR", "MRS", "MS",
            "MX", "OFC", "PHD", "PRES", "PROF", "REV", "SIR"
        };

        readonly static Dictionary<char, Int32[]> _concatCharMap = new Dictionary<char, Int32[]>
        {
            {'A', new []{0x00C4, 0x00E4, 0x00C0, 0x00E0, 0x00C1, 0x00E1,0x00C2,
                         0x00E2, 0x00C3, 0x00E3, 0x00C5, 0x00E5,0x01CD, 0x01CE,
                         0x0104, 0x0105, 0x0102, 0x0103,0x00C6, 0x00E6}},
            {'C', new []{0x00C7, 0x00E7, 0x0106, 0x0107, 0x0108, 0x0109, 0x010C, 0x010D}},
            {'D', new []{0x010E, 0x0111, 0x0110, 0x010F, 0x00F0}},
            {'E', new []{0x00C8, 0x00E8, 0x00C9, 0x00E9, 0x00CA, 0x00EA, 0x00CB, 0x00EB, 0x011A, 0x011B, 0x0118, 0x0119}},
            {'G', new []{0x011C, 0x011D, 0x0122, 0x0123, 0x011E, 0x011F}},
            {'H', new []{0x0124, 0x0124}},
            {'I', new []{0x00CC, 0x00EC, 0x00CD, 0x00ED, 0x00CE, 0x00EE,0x00CF, 0x00EF, 0x0131}},
            {'J', new []{0x0134, 0x0135}},
            {'K', new []{0x0136, 0x0137}},
            {'L', new []{0x0139, 0x013A, 0x013B, 0x013C, 0x0141, 0x0142, 0x013D, 0x013E}},
            {'N', new []{0x00D1, 0x00F1, 0x0143, 0x0144, 0x0147, 0x0148}},
            {'O', new []{0x00D6, 0x00F6, 0x00D2, 0x00F2, 0x00D3, 0x00F3, 0x00D4, 0x00F4,
                          0x00D5, 0x00F5, 0x0150, 0x0151, 0x00D8, 0x00F8, 0x0152, 0x0153}},
            {'R', new []{0x0154, 0x0155, 0x0158, 0x0159}},
            {'S', new []{0x1E9E, 0x00DF, 0x015A, 0x015B, 0x015C, 0x015D, 0x015E, 0x015F, 0x0160, 0x0161, 0x0218, 0x0219}},
            {'T', new []{0x0164, 0x0165, 0x0162, 0x0163, 0x00DE, 0x00FE, 0x021A, 0x021B}},
            {'U', new []{0x00DC, 0x00FC, 0x00D9, 0x00F9, 0x00DA, 0x00FA, 0x00DB, 0x00FB,
                         0x0170, 0x0171, 0x0168, 0x0169, 0x0172, 0x0173, 0x016E, 0x016F}},
            {'W', new []{0x0174, 0x0175}},
            {'Y', new []{0x00DD, 0x00FD, 0x0178, 0x00FF, 0x0176, 0x0177}},
            {'Z', new []{0x0179, 0x017A, 0x017D, 0x017E, 0x017B, 0x017C}}
        };
        #endregion

        #region Functions

        private bool ValidationCountryCode(string countryCode)
        {
            return _ccalpha2.Any(x => x.Contains(countryCode));
        }

        private string RemoveChars(string input)
        {
            return input.Replace(".", string.Empty).Replace(",", string.Empty).Replace(";", string.Empty);
        }

        private string RemovePrefix(string input)
        {
            var target = input.Trim().ToUpperInvariant();
            foreach (var prefix in _prefix2)
            {
                if (target.StartsWith(prefix + " "))
                {
                    return target.Substring(prefix.Length, target.Length - prefix.Length);
                }
            }

            foreach (var prefix in _prefix)
            {
                if (target.StartsWith(prefix + " "))
                {
                    return target.Substring(prefix.Length, target.Length - prefix.Length);
                }
            }

            return target;
        }

        private string RemoveTitle(string input)
        {
            var target = input.Trim().ToUpperInvariant();
            foreach (var title in _titles)
            {
                if (target.StartsWith(title + " "))
                {
                    return target.Substring(title.Length, target.Length - title.Length);
                }
            }

            return target;
        }

        private string CharacterRewrite(string input)
        {
            // Apply the A-Z untouched, apply charactermap, any other char is deleted
            var control = input.ToUpperInvariant();
            var result = new StringBuilder(string.Empty);

            foreach (char c in control)
            {
                char? valid = null;
                if ((char.ToUpperInvariant(c) >= 'A' && char.ToUpperInvariant(c) <= 'Z'))
                {
                    valid = c;
                }
                else
                {
                    var unicode = Convert.ToInt32(c);
                    var checkCharacter = _concatCharMap.Where(x => x.Value.Contains(unicode));

                    if (checkCharacter.Any())
                        valid = checkCharacter.First().Key;
                }

                if (!valid.HasValue) continue;
                result.Append((char)valid);
            }
            return result.ToString();
        }

        private string CapAndPad(string input)
        {
            if (input.Length < 5)
            {
                return input.PadRight(5, '#');
            }
            return input.Substring(0, 5);
        }
        #endregion

        public string Generate(ClientInformation clientInformation)
        {
            if (clientInformation == null)
                throw new ArgumentNullException(nameof(clientInformation));

            if (string.IsNullOrWhiteSpace(clientInformation.Firstname))
                throw new ArgumentException($"{nameof(clientInformation.Firstname)} cannot be empty");

            if (string.IsNullOrWhiteSpace(clientInformation.Surname))
                throw new ArgumentException($"{nameof(clientInformation.Surname)} cannot be empty");

            if (!ValidationCountryCode(clientInformation.CountryCode))
                throw new CountryCodeInvalidException();

            var firstName = clientInformation.Firstname.Trim();
            var lastName = clientInformation.Surname.Trim();
            var countryCode = clientInformation.CountryCode.Trim();
            var birthDate = clientInformation.DateofBirth.ToString("yyyyMMdd");

            firstName = RemoveChars(firstName);
            lastName = RemoveChars(lastName);

            firstName = RemoveTitle(firstName);
            firstName = RemovePrefix(firstName);
            lastName = RemoveTitle(lastName);
            lastName = RemovePrefix(lastName);

            // if firstanme contains more names like "Erwin Rudolf Josef Alexander", split
            // on " " to separate first names. This seems in alignment with specifications
            // that emphasis "first name".
            firstName = firstName.Trim().Split(' ')[0];
            firstName = firstName.Trim().Replace(" ", "");
            lastName = lastName.Trim().Replace(" ", "");

            var firstnamepart = CapAndPad(CharacterRewrite(firstName));
            var lastnamepart = CapAndPad(CharacterRewrite(lastName));
            var concat = string.Concat(countryCode, birthDate, firstnamepart, lastnamepart).ToUpperInvariant();

            return concat;
        }
    }
}
