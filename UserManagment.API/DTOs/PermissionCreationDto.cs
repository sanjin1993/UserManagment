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

        [Required(ErrorMessage = "You should fill out a UserId.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "You should fill out a PermissionId.")]
        public Guid PermissionId { get; set; }
    }
}
