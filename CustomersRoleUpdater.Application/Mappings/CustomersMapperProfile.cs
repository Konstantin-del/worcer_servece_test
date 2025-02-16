
using AutoMapper;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Mappings;

public class CustomersMapperProfile : Profile
{
    public CustomersMapperProfile()
    {
        CreateMap<Customer, Guid>().ReverseMap();
       // CreateMap<Customer, CustomerId>().ForMember(dest =>
            //dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
