namespace API.App.Authorization.JWT;

public class AuthenticationResponse
{
	public string? JwtToken { get; set; }

	public static AuthenticationResponse Empty()
    {
        return new AuthenticationResponse()
        {
			JwtToken = null
		};

	}
}

