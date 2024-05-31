using FluentValidation.Validators;

namespace FluentValidation.WebAPI;

public static class ExtensionMethods
{
    public static bool CheckNullEmptyAndMinLength(this string value, int minLength = 3)
    {
        if (string.IsNullOrEmpty(value)) return false;

        if (value.Length < minLength) return false;

        return true;
    }

    public static IRuleBuilderOptions<T, string> DoluOlmali<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new DoluOlmaliValidator<T>()); ;
    }
}


public class DoluOlmaliValidator<T> : PropertyValidator<T, string>
{

    public override string Name => "DoluOlmaliValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (value == null)
        {
            return false;
        }

        if (value is string s && string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        if (value is string col && col.Length < 3)
        {
            return false;
        }


        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "Name en az 3 karakter olmalıdır!";
    }
}
