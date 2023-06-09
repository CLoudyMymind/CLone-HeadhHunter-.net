using System.ComponentModel.DataAnnotations;

namespace HeadHunterVer1._0.Attributes;

public class CustomValidateExperience : ValidationAttribute
{
    private readonly string _fromPropertyName;
    private readonly string _toPropertyName;

    public CustomValidateExperience(string fromPropertyName, string toPropertyName)
    {
        _fromPropertyName = fromPropertyName;
        _toPropertyName = toPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var fromProperty = validationContext.ObjectType.GetProperty(_fromPropertyName);
        var toProperty = validationContext.ObjectType.GetProperty(_toPropertyName);

        if (fromProperty != null && toProperty != null)
        {
            var fromValue = (int?)fromProperty.GetValue(validationContext.ObjectInstance);
            var toValue = (int?)toProperty.GetValue(validationContext.ObjectInstance);

            if (fromValue.HasValue && toValue.HasValue && fromValue > toValue)
            {
                return new ValidationResult("Значение опыта работы 'от' не должно быть больше значения опыта работы 'до'.");
            }
        }

        return ValidationResult.Success;
    }
}