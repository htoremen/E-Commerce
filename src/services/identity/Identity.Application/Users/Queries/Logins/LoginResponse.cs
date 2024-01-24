namespace Identity.Application.Users;

public class LoginResponse
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }

    public LoginResponse(User user, string jwtToken, string refreshToken)
    {
        UserId = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}
