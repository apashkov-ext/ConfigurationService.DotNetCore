using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Api.Extensions;
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
                obj[opt.Name.Value.ToLowerCamelCase()] = ParseValue(opt.Value.Value, opt.Value.Type);
            }

            foreach (var nested in source.NestedGroups)
            {
                obj[nested.Name.Value.ToLowerCamelCase()] = JsObject.Create(nested);
            }

            return obj;
        }

        private static object ParseValue(string value, OptionValueType type)
        {
            return type switch
            {
                OptionValueType.String => value,
                OptionValueType.Number => int.Parse(value),
                OptionValueType.Boolean => bool.Parse(value),
                OptionValueType.StringArray => value.Split(','),
                OptionValueType.NumberArray => value.Split(',').Select(int.Parse),
                _ => throw new ApplicationException("Unsupported property type")
            };
        }
    }
}