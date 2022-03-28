using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.API.Services;

namespace UserManagment.API.Controllers
{

    [ApiController]
    [Route("api/users/{userId}/permissions")]
    public class PermissionsController : Controller
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IPermissionRepository _permissionRepository;
        private IMapper _mapper;

        public PermissionsController(ILogger<PermissionsController> logger,
            IPermissionRepository permissionRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
    }
}
