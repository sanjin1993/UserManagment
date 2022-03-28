using Books.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Contexts
{
    public class UserManagmentContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public UserManagmentContext(DbContextOptions<UserManagmentContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserPermission>().HasKey(up => new { up.UserId, up.PermissionId });

            modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<int>();


            modelBuilder.Entity<Permission>().HasData(
                 new Permission()
                 {
                     Id = Guid.Parse("7646143D-7781-4566-BF49-5ABA3751BFA4"),
                     Code = "R--",
                     Description = "only read permission"
                 },
                new Permission()
                {
                    Id = Guid.Parse("5248737E-7105-4B59-BE5D-47C3BC72A767"),
                    Code = "-W-",
                    Description = "only write permission"
                },
                new Permission()
                {
                    Id = Guid.Parse("AC0D40AF-F0DD-44BD-AD5D-83EE2BCFA051"),
                    Code = "--X",
                    Description = "only execute permission"
                },
                new Permission()
                {
                    Id = Guid.Parse("35D77900-E205-4118-9E0A-5B4DA53F741E"),
                    Code = "RWX",
                    Description = "all permissions granted"
                },
                 new Permission()
                 {
                     Id = Guid.Parse("AEF3C363-DBCB-43B9-99FF-197F2DBF9E12"),
                     Code = "---",
                     Description = "no permissions"
                 });


            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("76B3712D-DCAD-4755-B7BE-C960CE44C395"),
                    Email = "John@gmail.com",
                    FirstName = "John",
                    LastName = "Wayne",
                    Password = "john123",
                    Status = User.UserStatus.WithoutPermission,
                    Username = "john123"
                },
                 new User()
                 {
                     Id = Guid.Parse("437201CC-8F91-41AA-9B15-99B97DE0C229"),
                     Email = "Ana@gmail.com",
                     FirstName = "Ana",
                     LastName = "Smith",
                     Password = "anaSmith1",
                     Status = User.UserStatus.Unlimited,
                     Username = "ana4"
                 },
                  new User()
                  {
                      Id = Guid.Parse("3C8F1DA3-9A18-4281-827F-6E4CAD52D675"),
                      Email = "Sasa@gmail.com",
                      FirstName = "Joe",
                      LastName = "Doe",
                      Password = "Joe23",
                      Status = User.UserStatus.Limited,
                      Username = "Joe45"
                  }
                );

            modelBuilder.Entity<UserPermission>().HasData(
                new UserPermission()
                {
                    PermissionId = Guid.Parse("AEF3C363-DBCB-43B9-99FF-197F2DBF9E12"),
                    UserId = Guid.Parse("76B3712D-DCAD-4755-B7BE-C960CE44C395")
                },

                 new UserPermission()
                 {
                     PermissionId = Guid.Parse("35D77900-E205-4118-9E0A-5B4DA53F741E"),
                     UserId = Guid.Parse("437201CC-8F91-41AA-9B15-99B97DE0C229")
                 },

                  new UserPermission()
                  {
                      PermissionId = Guid.Parse("5248737E-7105-4B59-BE5D-47C3BC72A767"),
                      UserId = Guid.Parse("3C8F1DA3-9A18-4281-827F-6E4CAD52D675")
                  },

                  new UserPermission()
                  {
                      PermissionId = Guid.Parse("AC0D40AF-F0DD-44BD-AD5D-83EE2BCFA051"),
                      UserId = Guid.Parse("3C8F1DA3-9A18-4281-827F-6E4CAD52D675")
                  }
            );
            base.OnModelCreating(modelBuilder);

        }
    }
}
