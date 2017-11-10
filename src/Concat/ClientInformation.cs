using System;

namespace Mifir.Concat
{
    public class ClientInformation
    {
        public ClientInformation(string firstname, string surname, string countryCode, DateTime dateofBirth)
        {
            Firstname = firstname;
            Surname = surname;
            CountryCode = countryCode;
            DateofBirth = dateofBirth;
        }

        public string Firstname { get; private set; }
        public string Surname { get; private set; }
        public string CountryCode { get; private set; }
        public DateTime DateofBirth { get; private set; }
    }
}
