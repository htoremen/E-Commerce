using Domain.Common;

namespace Domain.Entities;

public class Parameter : BaseEntity
{
    public string Name { get; set; } 

    public string ParameterTypeId { get; set; }
    public bool IsActive { get; set; }
    public virtual ParameterType ParameterType { get; set; }
}