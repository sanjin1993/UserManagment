using Books.API.Contexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UserManagment.API.DTOs;
using UserManagment.API.Services;
using X.PagedList;

namespace Books.API.Services
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private UserManagmentContext _context;
        private readonly ILogger<UserRepository> _logger;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly DbSet<User> _query;

        public UserRepository(UserManagmentContext context,
            ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _query = _context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.UserPermissions)
                                       .ThenInclude(p => p.Permission)
                                       .ToListAsync();
        }

        public async Task<IPagedList<User>> GetUsersAsync(RequestParams userParams)
        {
            return await _context.Users.Include(u => u.UserPermissions)
                                         .ThenInclude(p => p.Permission)
                                         .OrderBy(o => o.FirstName)
                                         .ToPagedListAsync(userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IPagedList<User>> SearchUsersAsync(Expression<Func<User, bool>> expression = null, UserRequestParams userParams = null)
        {
            IQueryable<User> query = _query;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            //if (userParams.OrderBy != null)
            //{
            //    ApplySort(ref query, userParams.OrderBy);
            //}

            return await query.Include(u => u.UserPermissions)
                              .ThenInclude(p => p.Permission)
                              .AsNoTracking()
                              .ToPagedListAsync(userParams.PageNumber, userParams.PageSize);
        }

        private void ApplySort(ref IQueryable<User> users, string orderByQueryString)
        {
            if (!users.Any())
                return;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                users = users.OrderBy(x => x.FirstName);
                return;
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                users = users.OrderBy(x => x.FirstName);
                return;
            }
            //users = users.OrderBy(orderQuery);
        }

        public async Task Insert(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _context.AddAsync(user);
        }

        public async Task<User> GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Users
              .Where(c => c.Id == userId).FirstOrDefaultAsync();
        }

        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return (await _context.SaveChangesAsync() > 0);
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
