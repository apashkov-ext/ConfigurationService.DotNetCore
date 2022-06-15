using AutoMapper;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Mappers;

public class OptionGroupMapperProfile : Profile
{
    public OptionGroupMapperProfile()
    {
        CreateMap<OptionGroupEntity, OptionGroupDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.ParentId, o => o.MapFrom<OptionGroupParentIdValueResolver>())
            .ForMember(dest => dest.ConfigurationId, o => o.MapFrom(src => src.Configuration.Id))
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Root, o => o.MapFrom<OptionGroupIsRootValueResolver>());
    }
}

public class OptionGroupParentIdValueResolver : IValueResolver<OptionGroupEntity, OptionGroupDto, string>
{
    public string Resolve(OptionGroupEntity source, OptionGroupDto destination, string destMember, ResolutionContext context)
    {
        return source.Parent?.Id.ToString();
    }
}

public class OptionGroupIsRootValueResolver : IValueResolver<OptionGroupEntity, OptionGroupDto, bool>
{
    public bool Resolve(OptionGroupEntity source, OptionGroupDto destination, bool destMember, ResolutionContext context)
    {
        return source.Parent == null;
    }
}
