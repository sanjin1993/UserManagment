using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Entities
{
    [Table("Users")]
    public class User
    {
        public enum UserStatus
        {
            Unlimited,
            Limited,
            WithoutPermission
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
            = new List<UserPermission>();
    }
}
