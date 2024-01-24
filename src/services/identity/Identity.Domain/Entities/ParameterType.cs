namespace Identity.Domain.Entities;

public class ParameterType : BaseEntity
{
    public ParameterType()
    {
        Parameters = new HashSet<Parameter>();
    }

    public string Name { get; set; }

    public virtual ICollection<Parameter> Parameters { get; set;}
}
