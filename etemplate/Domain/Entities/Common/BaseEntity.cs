using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Common;

public abstract class BaseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastModifiedDate { get; set; }
}
