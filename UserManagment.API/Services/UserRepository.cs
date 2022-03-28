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


        //public async Task<IEnumerable<BookCover>> DownloadBookCoversAsync(Guid bookId) 
        //{
        //    var bookCoverUrls = new[]
        //    {
        //     $"http://localhost:52644/api/bookcovers/{bookId}-dummycover1",
        //     $"http://localhost:52644/api/bookcovers/{bookId}-dummycover2"
        //    };

        //    var bookCovers = new List<BookCover>();
        //    var downloadTask1 = DownloadBookCoverAsync(bookCoverUrls[0], bookCovers);
        //    var downloadTask2 = DownloadBookCoverAsync(bookCoverUrls[1], bookCovers);
        //    await Task.WhenAll(downloadTask1, downloadTask2);
        //    return bookCovers;
        //}

        //private async Task DownloadBookCoverAsync(string bookCoverUrl, List<BookCover> bookCovers)
        //{
        //    var response = await _httpClient.GetAsync(bookCoverUrl);
        //    var bookCover = JsonSerializer.Deserialize<BookCover>(
        //         await response.Content.ReadAsStringAsync(),
        //         new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true,
        //         });

        //    bookCovers.Add(bookCover);
        //}

        //public async Task<Book> GetBookAsync(Guid id)
        //{
        //    //var pageCalculator = new Books.Legacy.ComplicatedPageCalculator();
        //    //var amountOfPages = pageCalculator.CalculateBookPages();
        //    _logger.LogInformation($"ThreadId when entering GetBookAsync: " +
        //        $"{System.Threading.Thread.CurrentThread.ManagedThreadId}");

        //    return await _context.Books
        //        .Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        //}

        //public IEnumerable<Book> GetBooks()
        //{
        //    _context.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:02';");
        //    return _context.Books.Include(b => b.Author).ToList();
        //}

        //public async Task<IEnumerable<Book>> GetBooksAsync()
        //{
        //    await _context.Database.ExecuteSqlRawAsync("WAITFOR DELAY '00:00:02';");
        //    return await _context.Books.Include(b => b.Author).ToListAsync();
        //}

        //public async Task<IEnumerable<Entities.Book>> GetBooksAsync(
        //    IEnumerable<Guid> bookIds)
        //{
        //    return await _context.Books.Where(b => bookIds.Contains(b.Id))
        //        .Include(b => b.Author).ToListAsync();
        //}

        //public async Task<BookCover> GetBookCoverAsync(string coverId)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    // pass through a dummy name
        //    var response = await httpClient
        //           .GetAsync($"http://localhost:52644/api/bookcovers/{coverId}");
        //    if (response.IsSuccessStatusCode)
        //    { 
        //        return JsonSerializer.Deserialize<BookCover>(
        //            await response.Content.ReadAsStringAsync(),
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true,
        //            });
        //    }

        //    return null;
        //}

        //private async Task<BookCover> DownloadBookCoverAsync(
        //    HttpClient httpClient, string bookCoverUrl, 
        //    CancellationToken cancellationToken)
        //{
        //    //throw new Exception("Cannot download book cover, " +
        //    //    "writer isn't finishing book fast enough.");

        //    var response = await httpClient
        //               .GetAsync(bookCoverUrl, cancellationToken);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var bookCover = JsonSerializer.Deserialize<BookCover>(
        //            await response.Content.ReadAsStringAsync(),
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true,
        //            });
        //        return bookCover;
        //    }

        //    _cancellationTokenSource.Cancel();
        //    return null;
        //}


        //public async Task<IEnumerable<BookCover>> GetBookCoversAsync(Guid bookId)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var bookCovers = new List<BookCover>();
        //    _cancellationTokenSource = new CancellationTokenSource();

        //    // create a list of fake bookcovers
        //    var bookCoverUrls = new[]
        //    {
        //        $"http://localhost:52644/api/bookcovers/{bookId}-dummycover1",
        //        // $"http://localhost:52644/api/bookcovers/{bookId}-dummycover2?returnFault=true",
        //        $"http://localhost:52644/api/bookcovers/{bookId}-dummycover2",
        //        $"http://localhost:52644/api/bookcovers/{bookId}-dummycover3",
        //        $"http://localhost:52644/api/bookcovers/{bookId}-dummycover4",
        //        $"http://localhost:52644/api/bookcovers/{bookId}-dummycover5"
        //    };

        //    // create the tasks
        //    var downloadBookCoverTasksQuery =
        //         from bookCoverUrl
        //         in bookCoverUrls
        //         select DownloadBookCoverAsync(httpClient, bookCoverUrl,
        //         _cancellationTokenSource.Token);

        //    // start the tasks
        //    var downloadBookCoverTasks = downloadBookCoverTasksQuery.ToList();
        //    try
        //    {       
        //        return await Task.WhenAll(downloadBookCoverTasks);
        //    }
        //    catch (OperationCanceledException operationCanceledException)
        //    {
        //        _logger.LogInformation($"{operationCanceledException.Message}");
        //        foreach (var task in downloadBookCoverTasks)
        //        {
        //            _logger.LogInformation($"Task {task.Id} has status {task.Status}");
        //        }

        //        return new List<BookCover>();
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError($"{exception.Message}");
        //        throw;
        //    }

        //    //foreach (var bookCoverUrl in bookCoverUrls)
        //    //{
        //    //    var response = await httpClient
        //    //       .GetAsync(bookCoverUrl);

        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        bookCovers.Add(JsonSerializer.Deserialize<BookCover>(
        //    //            await response.Content.ReadAsStringAsync(),
        //    //            new JsonSerializerOptions
        //    //            {
        //    //                PropertyNameCaseInsensitive = true,
        //    //            }));
        //    //    }
        //    //}

        //    //return bookCovers;
        //}

        //public void AddBook(Book bookToAdd)
        //{
        //    if (bookToAdd == null)
        //    {
        //        throw new ArgumentNullException(nameof(bookToAdd));
        //    }

        //    _context.Add(bookToAdd);
        //}



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

        public async Task<IPagedList<User>> SearchUsersAsync(Expression<Func<User, bool>> expression = null,  UserRequestParams userParams = null)
        {
            IQueryable<User> query = _query;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (userParams.OrderBy != null)
            {
                ApplySort(ref query, userParams.OrderBy);
            }

            return await query.Include(u => u.UserPermissions)
                        .ThenInclude(p => p.Permission).AsNoTracking().ToPagedListAsync(userParams.PageNumber, userParams.PageSize);
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
            users = users.OrderBy(x => orderQuery);
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
