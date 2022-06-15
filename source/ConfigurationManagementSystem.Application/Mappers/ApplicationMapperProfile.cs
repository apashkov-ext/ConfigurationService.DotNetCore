using AutoMapper;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Mappers;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<ApplicationEntity, ApplicationDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name.Value));

        CreateMap<ApplicationEntity, CreatedApplicationDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.ApiKey, o => o.MapFrom(src => src.ApiKey.Value));
    }
}
