using AutoMapper;
using AVS.Contacts.Contracts.DTOs;
using AVS.Contacts.Domain.Entities;

namespace AVS.Contacts.Application.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactDto>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.LastName))
            .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
            .ForMember(d => d.Number, o => o.MapFrom(s => s.Address.Number))
            .ForMember(d => d.District, o => o.MapFrom(s => s.Address.District))
            .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
            .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone.ToString()));
    }
}