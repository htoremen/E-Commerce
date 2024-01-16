namespace Domain.Entities.Common;

public abstract class BaseEntity
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastModifiedDate { get; set; }
}
