using AutoMapper;
using Books.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.API.DTOs;
using UserManagment.API.Services;

namespace UserManagment.API.Controllers
{

    [ApiController]
    //[Route("api/users/{userId}/permissions")]
    public class PermissionsController : Controller
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public PermissionsController(ILogger<PermissionsController> logger,
            IPermissionRepository permissionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/users/{userId}/permissions")]
        public async Task<IActionResult> GetUserPermissions(Guid userId)
        {
            try
            {
                if (!_userRepository.UserExists(userId))
                {
                    _logger.LogInformation($"User with id {userId} wasn't found");
                    return NotFound();
                }

                var permissionsForUser = await _permissionRepository.GetPermissionsForUser(userId);

                return Ok(_mapper.Map<IEnumerable<PermissionDto>>(permissionsForUser));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting permissions for user with id {userId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost("api/users/{userId}/permissions/{permissionId}")]
        public async Task<IActionResult> AddUserPermission(Guid userId, Guid permissionId)
        {
            await _permissionRepository.Insert(userId, permissionId);
            await _permissionRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("api/permissions/{permissionId}")]
        public async Task<IActionResult> DeletePermission(Guid permissionId)
        {
            if (Guid.Empty == permissionId)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePermission)}");
                return BadRequest();
            }
            var permissionFromRepo = await _permissionRepository.GetPermission(permissionId);

            if (permissionFromRepo == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePermission)}");
                return BadRequest("Submitted data is invalid");
            }

            _permissionRepository.Delete(permissionFromRepo);
            await _permissionRepository.SaveChangesAsync();

            return NoContent();
        }


    }
}
