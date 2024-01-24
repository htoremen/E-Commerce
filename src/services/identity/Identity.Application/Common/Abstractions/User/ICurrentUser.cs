namespace Identity.Application.Common.Abstractions.User
{
    public interface ICurrentUser
    {
        string UserId { get; }
    }
}
