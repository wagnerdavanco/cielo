using Cielo.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cielo.Validators
{
    public static class CredcardValidator
    {
        public static bool IsValid(string cardNumber, EnumBrand brand)
        {
            // Visa
            if (Regex.IsMatch(cardNumber, "^(4)")
                && ((brand & EnumBrand.Visa) != 0))
                return cardNumber.Length == 13 || cardNumber.Length == 16;

            // MasterCard
            if (Regex.IsMatch(cardNumber, "^(51|52|53|54|55)")
                && ((brand & EnumBrand.Master) != 0))
                return cardNumber.Length == 16;

            // Amex
            if (Regex.IsMatch(cardNumber, "^(34|37)")
                && ((brand & EnumBrand.Amex) != 0))
                return cardNumber.Length == 15;

            // Diners
            if (Regex.IsMatch(cardNumber, "^(300|301|302|303|304|305|36|38)")
                && ((brand & EnumBrand.Diners) != 0))
                return cardNumber.Length == 14;

            return false;
        }
    }
}
