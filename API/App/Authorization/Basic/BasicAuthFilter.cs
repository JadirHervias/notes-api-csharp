using System.Net;
using System.Net.Http.Headers;
using System.Text;
using API.Domain.User;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.App.Authorization.Basic;

public class BasicAuthFilter : IAuthorizationFilter
{
    private readonly string _realm;

    public BasicAuthFilter(string realm)
    {
        _realm = realm;

        if (string.IsNullOrWhiteSpace(_realm))
        {
            throw new ArgumentNullException(nameof(realm), "Please provide a non-empty realm value.");
        }
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
                if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var credentials = Encoding.UTF8
                                        .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
                                        .Split(':', 2);
                    if (credentials.Length == 2)
                    {
                        if (IsAuthorized(context, credentials[0], credentials[1]))
                        {
                            return;
                        }
                    }
                }
            }

            ReturnUnauthorizedResult(context);
        }
        catch (FormatException)
        {
            ReturnUnauthorizedResult(context);
        }
    }

    public bool IsAuthorized(AuthorizationFilterContext context, string username, string password)
    {
        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        var user = Task.Run(() => userService.SearchByUserName(username)).Result;

        if (null == user || !typeof(UserEntity).IsInstanceOfType(user))
        {
            return false;
        }

        PasswordHasher<UserEntity> hasher = new PasswordHasher<UserEntity>();

        PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, password);


        if (result == PasswordVerificationResult.Failed)
        {
            return false;
        }

        return true;
    }

    private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
    {
        context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{_realm}\"";
        context.Result = new UnauthorizedResult();
    }
}

