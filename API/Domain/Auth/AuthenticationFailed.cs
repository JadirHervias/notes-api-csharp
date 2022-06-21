using API.Domain.Exceptions;

namespace API.Domain.Auth;

public class AuthenticationFailed : DomainException
{
	public AuthenticationFailed() : base("Wrong credentials. Authentication failed.")
	{
	}
}

