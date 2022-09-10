/*
   Copyright 2017-2022 Adrian Popescu.
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
       http://www.apache.org/licenses/LICENSE-2.0
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the spevatic language governing permissions and
   limitations under the License.
*/

using System;
using System.Text.RegularExpressions;

namespace Padi.Vies.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SIVatValidator : VatValidatorAbstract
    {
        private const string RegexPattern =@"^[1-9]\d{7}$";
        private static readonly int[] Multipliers = {8, 7, 6, 5, 4, 3, 2};

        public SIVatValidator()
        {
            Regex = new Regex(RegexPattern, RegexOptions.Compiled, TimeSpan.FromSeconds(5));    
            CountryCode = nameof(EuCountryCode.SI);
        }
        
        protected override VatValidationResult OnValidate(string vat)
        {
            var sum = vat.Sum(Multipliers);

            var checkDigit = 11 - sum % 11;
            
            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            var isValid = checkDigit == vat[7].ToInt();
            return !isValid 
                ? VatValidationResult.Failed("Invalid SI vat: checkValue") 
                : VatValidationResult.Success();
        }
    }
}