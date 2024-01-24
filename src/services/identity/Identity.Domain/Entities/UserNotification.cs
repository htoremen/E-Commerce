namespace Identity.Domain.Entities;

public class UserNotification : BaseEntity
{
    public string UserId { get; set; }
    public int ParameterId { get; set; }
    public bool IsActive { get; set; }  

    public virtual User User { get; set; }
    public virtual Parameter Parameter { get; set; }
}
