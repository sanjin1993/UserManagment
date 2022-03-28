using Books.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagment.API.DTOs;
using X.PagedList;

namespace UserManagment.API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IPagedList<User>> GetUsersAsync(RequestParams userParams);

        Task<IPagedList<User>> SearchUsersAsync(
           Expression<Func<User, bool>> expression = null,
           UserRequestParams userParams = null
        );

        //Task<User> GetBookAsync(Guid id);

        //Task<IEnumerable<User>> GetBooksAsync(IEnumerable<Guid> userIds);

        //void AddBook(User userToAdd);

        //Task<bool> SaveChangesAsync();
    }
}
