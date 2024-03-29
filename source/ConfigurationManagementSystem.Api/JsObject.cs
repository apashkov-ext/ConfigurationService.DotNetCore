﻿using System;
using System.Collections.Generic;
using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api;

internal class JsObject : Dictionary<string, object>
{
    public new object this[string key]
    {
        get => !ContainsKey(key) ? null : base[key];
        set => base[key] = value;
    }

    public JsObject Merge(JsObject obj)
    {
        foreach (var (key, value) in this)
        {
            this[key] = value;
        }

        return this;
    }

    public static JsObject Create(OptionGroupEntity source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var obj = new JsObject();

        foreach (var opt in source.Options)
        {
            var key = opt.Name.Value.ToLowerCamelCase();
            var value = TypeConversion.Parse(opt.Value.Value, opt.Value.Type);
            obj[key] = value;
        }

        foreach (var nested in source.NestedGroups)
        {
            var key = nested.Name.Value.ToLowerCamelCase();
            var value = Create(nested);
            obj[key] = value;
        }

        return obj;
    }
}
