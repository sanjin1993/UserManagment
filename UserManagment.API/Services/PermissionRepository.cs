using Books.API.Contexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagment.API.Services
{
    public class PermissionRepository : IPermissionRepository, IDisposable
    {

        private UserManagmentContext _context;
        private readonly ILogger<PermissionRepository> _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public PermissionRepository(UserManagmentContext context,
           ILogger<PermissionRepository> logger )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId)
        {
            return await _context.UserPermissions.Where(p => p.UserId == userId).Select(u => u.Permission).ToListAsync();
        }

        public async Task Insert(Guid UserId, Guid PermissionId)
        {
            if (UserId == Guid.Empty || PermissionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Insert));
            }
            await _context.AddAsync(new UserPermission { PermissionId = PermissionId, UserId = UserId });
        }

        public void Delete(Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            _context.Permissions.Remove(permission);
        }

        public async Task<Permission> GetPermission(Guid permissionId)
        {
            if (permissionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(permissionId));
            }

            return await _context.Permissions
              .Where(p => p.Id == permissionId).FirstOrDefaultAsync();
        }
    }
}
