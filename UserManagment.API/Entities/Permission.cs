using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Entities
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
            = new List<UserPermission>();
    }
}
