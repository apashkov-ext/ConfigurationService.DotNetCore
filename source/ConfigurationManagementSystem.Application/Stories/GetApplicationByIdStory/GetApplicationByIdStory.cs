using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using AutoMapper;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory
{
    [Component]
    public class GetApplicationByIdStory
    {
        private readonly GetApplicationByIdWithoutHierarchyQuery _getApplicationByIdQuery;
        private readonly IMapper _mapper;

        public GetApplicationByIdStory(GetApplicationByIdWithoutHierarchyQuery getApplicationByIdQuery,
            IMapper mapper)
        {
            _getApplicationByIdQuery = getApplicationByIdQuery;
            _mapper = mapper;
        }

        public async Task<ApplicationDto> ExecuteAsync(Guid id)
        {
            var app = await _getApplicationByIdQuery.ExecuteAsync(id);
            if (app is null) throw new EntityNotFoundException("Application does not exist");
            return _mapper.Map<ApplicationDto>(app);
        }
    }
}
