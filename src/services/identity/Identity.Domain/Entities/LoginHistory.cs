namespace Identity.Domain.Entities;

public class LoginHistory : BaseEntity
{
    [JsonIgnore]
    public string LoginId { get; set; }
    public string UserId { get; set; }
    public bool IsLogin { get; set; }
    public DateTime CreatedAt { get; set; }
}
