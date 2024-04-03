using BookRental.DAL;
using BookRental.Model;
using BookRental.WebApi.DTO;
using BookRental.WebApi.JWT;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookRental.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(ITokenManager tokenManager, IAuthenticationRepository authenticationRepository)
        {
            _tokenManager = tokenManager;
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] CheckCredentialsDTO credentials)
        {
            // Validate credentials and generate token
            var user = _authenticationRepository.CheckCredentials(credentials.ToUser());
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var roleName = _authenticationRepository.GetUserRole(user.RoleId);
            var token = _tokenManager.GenerateToken(user, roleName);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] NewUserDTO newUser)
        {
            // Validate and create a new user
            var user = newUser.ToUser();
            int result = _authenticationRepository.RegisterUser(user);
            if (result > 0)
            {
                return Ok();
            }

            return BadRequest("User registration failed");
        }

        [HttpPut("updateToken")]
        public async Task<IActionResult> UpdateTokenAvailable([FromBody] UpdateTokenAvailableDTO updateToken)
        {
            // Get user and update token availability
            var user = await _authenticationRepository.GetUserById(updateToken.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Token_Available += updateToken.AddTokens; // Example: Add tokens
            int result = await _authenticationRepository.UpdateUser(user);
            if (result > 0)
            {
                return Ok(new { newTokenAvailable = user.Token_Available });
            }

            return BadRequest("Token availability update failed");
        }
    }
}
