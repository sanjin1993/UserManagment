using AutoMapper;
using Books.API.Filters;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.API.CustomExceptions;
using UserManagment.API.DTOs;
using UserManagment.API.Errors;
using UserManagment.API.Services;

namespace Books.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UsersController(ILogger<UsersController> logger,
            IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [UsersResultFilter]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();

            //if (users.Count() <= 0)
            //    throw new NoUsersFoundException("Niti jedan user pronadjen");

            _logger.LogInformation($"Returning {users.Count()} users.");
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUsers")]
        [UsersResultFilter]
        public async Task<IActionResult> GetUsers([FromQuery] RequestParams request)
        {
            var users = await _userRepository.GetUsersAsync(request);

            _logger.LogInformation($"Returning {users.Count()} users.");
            return Ok(users);
        }

        [HttpGet]
        [Route("SearchUsers")]
        [UsersResultFilter]
        public async Task<IActionResult> SearchUsers([FromQuery] UserRequestParams request)
        {
            
            if(request.hasFilters)
            {
              var users  = await _userRepository.SearchUsersAsync(u => u.FirstName.Contains(request.firstName) || u.LastName.Contains(request.lastName) || u.Username.Equals(request.userName) || u.Email.Equals(request.emailAddress), request);

                _logger.LogInformation($"Returning {users.Count()} users.");

                return Ok(users);
            }

            return Ok( await _userRepository.GetUsersAsync(request));

        }

    }
}
