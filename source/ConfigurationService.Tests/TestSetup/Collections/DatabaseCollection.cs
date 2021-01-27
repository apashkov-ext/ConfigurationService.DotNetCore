using ConfigurationService.Tests.TestSetup.Fixtures;
using Xunit;

namespace ConfigurationService.Tests.TestSetup.Collections
{
    [CollectionDefinition(nameof(DbContextCollection))]
    public class DbContextCollection : ICollectionFixture<DbContextFixture>
    {
    }
}