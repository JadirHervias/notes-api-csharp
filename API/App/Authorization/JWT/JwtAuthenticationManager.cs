using API.App.Requests;
using API.Domain.Auth;
using API.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.App.Authorization.JWT;

public class JwtAuthenticationManager : IJwtAuthenticationManager
{
    private IConfiguration _config;
    private IUserRepository _userRepository;

    public JwtAuthenticationManager(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    public string Generate(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };

        var token = new JwtSecurityToken(
          _config["Jwt:Issuer"],
          _config["Jwt:Audience"],
          claims,
          expires: DateTime.Now.AddMinutes(5),
          signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserEntity> Authenticate(LoginRequest request)
    {
        UserEntity? user = await _userRepository.SearchByUsername(request.Username);

        if (null == user)
        {
            throw new AuthenticationFailed();
        }

        PasswordHasher<UserEntity> hasher = new PasswordHasher<UserEntity>();

        PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, request.Password);


        if (result == PasswordVerificationResult.Failed)
        {
            throw new AuthenticationFailed();
        }

        return user;
    }
}

public interface IJwtAuthenticationManager
{
    string Generate(UserEntity user);
    Task<UserEntity> Authenticate(LoginRequest request);
}
