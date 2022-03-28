using Books.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagment.API.Filters
{
    public class PermisionCodeMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var permission = (PermissionDto)validationContext.ObjectInstance;

            if (permission.Code == permission.Description)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(PermissionDto) });
            }

            return ValidationResult.Success;
        }
    }
}
