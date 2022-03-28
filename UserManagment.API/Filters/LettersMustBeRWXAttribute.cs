using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagment.API.Filters
{
    public class LettersMustBeRWXAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string[] stringarr = new string[] { "R--", "-W-", "--X", "RWX", "---" };

            var code = value.ToString();

            if (!stringarr.Contains(code))
            {
                return new ValidationResult($"Acceptable combination are {stringarr.ToString()}");
            }

            return ValidationResult.Success;
        }
    }
}
    