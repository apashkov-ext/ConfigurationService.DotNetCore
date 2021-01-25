using System;
using System.Collections.Generic;
using ConfigurationService.Api.Extensions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api
{
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

        public static JsObject Create(OptionGroup source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            var obj = new JsObject();

            foreach (var opt in source.Options)
            {
                obj[opt.Name.Value.ToLowerCamelCase()] = TypeConversion.Parse(opt.Value.Value, opt.Value.Type);
            }

            foreach (var nested in source.NestedGroups)
            {
                obj[nested.Name.Value.ToLowerCamelCase()] = Create(nested);
            }

            return obj;
        }
    }
}