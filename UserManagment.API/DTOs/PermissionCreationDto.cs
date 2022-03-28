using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.API.Filters;

namespace UserManagment.API.DTOs
{
    [PermisionCodeMustBeDifferentFromDescription(ErrorMessage = "Code must be different from desription")]
    public class PermissionCreationDto
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(5, ErrorMessage = "The code shouldnt have more that 5 characters")]
        [LettersMustBeRWX]
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
