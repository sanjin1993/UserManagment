using Books.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagment.API.Services
{
    public interface IPermissionRepository 
    {
        Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId);
        Task Insert(Guid UserId, Guid PermissionId);

        Task<bool> SaveChangesAsync();

        void Delete(Permission permission);

        Task<Permission> GetPermission(Guid permissionId);
    }
}
