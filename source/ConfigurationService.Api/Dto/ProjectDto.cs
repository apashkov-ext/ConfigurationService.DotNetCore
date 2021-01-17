﻿using System.Collections.Generic;

namespace ConfigurationService.Api.Dto
{
    public class ProjectDto
    {
        public string Name { get; set; }
        public IEnumerable<EnvironmentDto> Configurations { get; set; }
    }
}
