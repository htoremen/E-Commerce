using Core.Events.Customers;
using Customer.Application.Customers;

namespace Customer.Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        CreateMap<CreateCustomer, Domain.Entities.Customer>();
        CreateMap<ICreateCustomer, Domain.Entities.Customer>();
        CreateMap<ICreateCustomer, CreateCustomerCommand>();
    }
}
