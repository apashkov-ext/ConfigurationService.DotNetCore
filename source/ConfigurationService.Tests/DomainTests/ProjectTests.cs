using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Tests.DomainTests
{
    public class ProjectTests
    {
        [Fact]
        public void Create_NewCorrectEntity()
        {
            var project = Project.Create(new ProjectName("proj"), new ApiKey(Guid.NewGuid()));

            Assert.NotNull(project);
        }

        [Fact]
        public void AddEnvironment_NotExisted_ReturnsNewEnvEntity()
        {
            var project = Project.Create(new ProjectName("proj"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("dev"));

            Assert.Equal(project, env.Project);
        }

        [Fact]
        public void AddEnvironment_Existed_ThrowsException()
        {
            var project = Project.Create(new ProjectName("proj"), new ApiKey(Guid.NewGuid()));
            project.AddEnvironment(new EnvironmentName("dev"));

            Assert.Throws<ApplicationException>(() => project.AddEnvironment(new EnvironmentName("dev")));
        }
    }
}
