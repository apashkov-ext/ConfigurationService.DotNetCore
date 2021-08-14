using System;
using System.Collections.Generic;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence.ConfigImporting.Implementation;
using ConfigurationService.Tests.Fixtures;
using Xunit;

namespace ConfigurationService.Persistence.Tests.ConfigurationsTests
{
    public class ConfigTreeTests
    {
        [Fact]
        public void Apply_NotExistedGroup_ContextContainsNewGroup()
        {
            using var context = new ConfigurationServiceContext(InMemoryContextOptions.GetContextOptions());

            var original = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            context.OptionGroups.Add(original);

            var imported = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            var logging = imported.AddNestedGroup(new OptionGroupName("logging"), new Description(""));

            var originalTree = new ConfigTree(original, context);
            var importedTree = new ImportedTree(imported);

            originalTree.ReplaceNodes(importedTree);
            context.SaveChanges();

            Assert.Contains(logging, context.OptionGroups, new TestOptionGroupEqualityComparer());
        }

        [Fact]
        public void Apply_NotExistedOption_ContextContainsNewOption()
        {
            using var context = new ConfigurationServiceContext(InMemoryContextOptions.GetContextOptions());

            var original = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            context.OptionGroups.Add(original);

            var imported = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            var opt = imported.AddOption(new OptionName("someOption"), new Description(""), new OptionValue(true));

            var originalTree = new ConfigTree(original, context);
            var importedTree = new ImportedTree(imported);

            originalTree.ReplaceNodes(importedTree);
            context.SaveChanges();

            Assert.Contains(opt, context.Options, new TestOptionComparer());
        }

        [Fact]
        public void Apply_NotExistedGroupWithOption_ContextContainsNewOption()
        {
            using var context = new ConfigurationServiceContext(InMemoryContextOptions.GetContextOptions());

            var original = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            context.OptionGroups.Add(original);

            var imported = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            var logging = imported.AddNestedGroup(new OptionGroupName("logging"), new Description(""));
            var enabledOpt = logging.AddOption(new OptionName("enabled"), new Description(""), new OptionValue(true));

            var originalTree = new ConfigTree(original, context);
            var importedTree = new ImportedTree(imported);

            originalTree.ReplaceNodes(importedTree);
            context.SaveChanges();

            Assert.Contains(enabledOpt, context.Options, new TestOptionComparer());
        }

        [Fact]
        public void Apply_ExistedGroupWithNoChildren_ContextDoesNotContainTheeseChildren()
        {
            using var context = new ConfigurationServiceContext(InMemoryContextOptions.GetContextOptions());

            var original = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            var logging = original.AddNestedGroup(new OptionGroupName("logging"), new Description(""));
            context.OptionGroups.AddRange(original, logging);

            var imported = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);

            var originalTree = new ConfigTree(original, context);
            var importedTree = new ImportedTree(imported);

            originalTree.ReplaceNodes(importedTree);
            context.SaveChanges();

            Assert.DoesNotContain(logging, context.OptionGroups, new TestOptionGroupEqualityComparer());
        }

        [Fact]
        public void Apply_ExistedGroupWithNoOptions_ContextDoesNotContainTheeseOptions()
        {
            using var context = new ConfigurationServiceContext(InMemoryContextOptions.GetContextOptions());

            var original = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            var opt = original.AddOption(new OptionName("someOption"), new Description(""), new OptionValue(true));
            context.OptionGroups.Add(original);
            context.Options.Add(opt);

            var imported = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);

            var originalTree = new ConfigTree(original, context);
            var importedTree = new ImportedTree(imported);

            originalTree.ReplaceNodes(importedTree);
            context.SaveChanges();

            Assert.DoesNotContain(opt, context.Options, new TestOptionComparer());
        }
    }

    internal class TestOptionGroupEqualityComparer : IEqualityComparer<OptionGroup>
    {
        public bool Equals(OptionGroup x, OptionGroup y)
        {
            if (x == null && y == null)
            {
                return false;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Name.Value.Equals(y.Name.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(OptionGroup obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class TestOptionComparer : IEqualityComparer<Option>
    {
        public bool Equals(Option x, Option y)
        {
            if (x == null && y == null)
            {
                return false;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Name.Value.Equals(y.Name.Value, StringComparison.InvariantCultureIgnoreCase) && x.Value == y.Value;
        }

        public int GetHashCode(Option obj)
        {
            return obj.GetHashCode();
        }
    }
}
