using Core.Events.Customers;

namespace Customer.Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        CreateMap<CreateCustomer, Domain.Entities.Customer>();
        CreateMap<ICreateCustomer, Domain.Entities.Customer>();
    }
}
