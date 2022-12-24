using Microsoft.AspNetCore.Mvc;
using TokenService.WebApi.Models;
using TokenService.WebApi.Requests;
using TokenService.WebApi.Services;

namespace TokenService.WebApi.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenGeneratorService _tokenGeneratorService;

        public AuthenticationController(TokenGeneratorService tokenGeneratorService)
        {
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var user = UsersRepositoryMock.Get(request.Username, request.Password);

            if (user == null)
                return NotFound(new { Message = "Usuário ou senha inválidos" });

            var token = _tokenGeneratorService.GenerateToken(user);

            return Ok(token);
        }
    }
}
