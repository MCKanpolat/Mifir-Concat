using System;

namespace Mifir.Concat
{
    public class CountryCodeInvalidException : Exception
    {
        public CountryCodeInvalidException() : base("Country Code Is Invalid")
        {
        }
    }
}
