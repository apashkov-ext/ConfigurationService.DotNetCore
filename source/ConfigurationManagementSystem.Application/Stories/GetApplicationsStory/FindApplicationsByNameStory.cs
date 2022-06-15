using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Extensions;
using AutoMapper;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [Component]
    public class FindApplicationsByNameStory
    {
        private readonly IGetApplicationsWithoutHierarchyQuery _getApplicationsByNameQuery;
        private readonly IMapper _mapper;

        public FindApplicationsByNameStory(IGetApplicationsWithoutHierarchyQuery getApplicationsByNameQuery,
            IMapper mapper)
        {
            _getApplicationsByNameQuery = getApplicationsByNameQuery;
            _mapper = mapper;
        }

        public async Task<PagedResponseDto<ApplicationDto>> ExecuteAsync(PagedRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            var nameFilter = string.IsNullOrEmpty(request.Name) ? null : new ApplicationName(request.Name);
            var apps = await _getApplicationsByNameQuery.ExecuteAsync(nameFilter, PaginationOptions.Create(request.Offset, request.Limit));

            return apps.ToPagedResponseDto(x => _mapper.Map<ApplicationDto>(x));
        }
    }
}
