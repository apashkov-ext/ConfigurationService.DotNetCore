using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities;

public class ConfigurationEntity : DomainEntity
{
    public ConfigurationName Name { get; private set; }
    public bool IsDefault { get; }
    public ApplicationEntity Application { get; }

    private readonly List<OptionGroupEntity> _optionGroups = new List<OptionGroupEntity>();
    public IEnumerable<OptionGroupEntity> OptionGroups => _optionGroups;

    protected ConfigurationEntity() { }

    protected ConfigurationEntity(ConfigurationName name, ApplicationEntity application, bool isDefault)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Application = application ?? throw new ArgumentNullException(nameof(application));
        IsDefault = isDefault;
        SetMainOptionGroup();
    }

    public static ConfigurationEntity Create(ConfigurationName name, ApplicationEntity application)
    {
        return new ConfigurationEntity(name, application, false);
    }

    public void UpdateName(ConfigurationName name)
    {
        Name = name;
    }

    public OptionGroupEntity GetRootOptionGroop()
    {
        var root = _optionGroups.SingleOrDefault(x => x.Parent == null);
        if (root == null)
        {
            throw new InconsistentDataStateException("The root option group cannot be null");
        }
        return root;
    }

    private void SetMainOptionGroup()
    {
        var g = OptionGroupEntity.Create(new OptionGroupName(""), this);
        _optionGroups.Add(g);
    }

    public override string ToString()
    {
        var s = $"{nameof(ConfigurationEntity)} {{ Id={Id}, Name={Name.Value} }}";
        return s;
    }
}
