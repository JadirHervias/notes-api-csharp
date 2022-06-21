using API.App.Authorization.JWT;
using API.App.Requests;
using API.App.Responses;
using API.Domain.Auth;
using API.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    readonly IJwtAuthenticationManager _jwtAuthenticationManager;

    public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
    {
        _jwtAuthenticationManager = jwtAuthenticationManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            UserEntity user = await _jwtAuthenticationManager.Authenticate(request);
            var token = _jwtAuthenticationManager.Generate(user);

            return Ok(new LoginResponse(user.Id.ToString(), user.UserName, token));
        } catch(AuthenticationFailed ex)
        {
            return NotFound(ex.Message);
        }
    }
}

