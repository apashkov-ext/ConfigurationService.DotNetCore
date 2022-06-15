using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConfigurationManagementSystem.Application.Attributes;

/// <summary>
/// Specifies that a data field value should be a valid and a non-empty Guid.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ValidGuidAttribute : ValidationAttribute
{
    public ValidGuidAttribute() : base("'{0}' does not contain a valid guid")
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var input = Convert.ToString(value, CultureInfo.CurrentCulture);

        if (!Guid.TryParse(input, out Guid guid))
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        if (guid == Guid.Empty)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return null;
    }
}
