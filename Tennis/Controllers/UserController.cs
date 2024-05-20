using Microsoft.AspNetCore.Mvc;
using Tennis.Models.Request;
using Tennis.Services.Interfaces;

namespace Tennis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(UserRequest userRequest)
        {
            await _userService.CreateUserAsync(userRequest);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync (UserRequest userRequest)
        {
            var userValidated = await _userService.ValidateUserAsync(userRequest);
            var token = _authenticationService.GenerateToken(userValidated);
            return Ok(token);
        }
    }
}
