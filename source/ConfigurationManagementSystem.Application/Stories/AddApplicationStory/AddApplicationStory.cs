using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using AutoMapper;

namespace ConfigurationManagementSystem.Application.Stories.AddApplicationStory
{
    [Component]
    public class AddApplicationStory
    {
        private readonly GetApplicationByNameQuery _getApplicationByNameQuery;
        private readonly CreateApplicationCommand _createApplicationCommand;
        private readonly GetApplicationByIdWithoutHierarchyQuery _getApplicationByIdWithoutHierarchyQuery;
        private readonly IMapper _mapper;

        public AddApplicationStory(GetApplicationByNameQuery getApplicationByNameQuery,
            CreateApplicationCommand createApplicationCommand,
            GetApplicationByIdWithoutHierarchyQuery getApplicationByIdWithoutHierarchyQuery,
            IMapper mapper)
        {
            _getApplicationByNameQuery = getApplicationByNameQuery;
            _createApplicationCommand = createApplicationCommand;
            _getApplicationByIdWithoutHierarchyQuery = getApplicationByIdWithoutHierarchyQuery;
            _mapper = mapper;
        }

        public async Task<CreatedApplicationDto> ExecuteAsync(string name)
        {
            var appName = new ApplicationName(name);
            var existed = await _getApplicationByNameQuery.ExecuteAsync(appName);
            if (existed != null)
            {
                throw new AlreadyExistsException("Application with the same name already exists");
            }

            var id = await _createApplicationCommand.ExecuteAsync(appName, new ApiKey(Guid.NewGuid()));
            var created = await _getApplicationByIdWithoutHierarchyQuery.ExecuteAsync(id);

            return _mapper.Map<CreatedApplicationDto>(created);
        }
    }
}
