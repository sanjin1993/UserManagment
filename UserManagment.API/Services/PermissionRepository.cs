using Books.API.Contexts;
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
    }
}
