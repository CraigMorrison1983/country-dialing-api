using System;
using System.Collections.Generic;
using System.Text;

namespace CountryDialingCode.Core.Utils
{
    public class PhoneNumberUtils
    {
        public static string GetCallingCodeFromPhoneNumber(string phoneNumber)
        {
            // Strip out spaces and + sign so we just working with the numbers
            phoneNumber = phoneNumber.Replace(" ", "");
            phoneNumber = phoneNumber.Replace("+", "");

            return phoneNumber.Remove(phoneNumber.Length - 10, 10);
        }
    }
}
