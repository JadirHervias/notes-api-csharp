namespace API.App.Responses;

public class LoginResponse
{
    public string Id { get; }
    public string Username { get; }
    public string Token { get; }

    public LoginResponse(string id, string userName, string token)
    {
        Id = id;
        Username = userName;
        Token = token;
    }
}

