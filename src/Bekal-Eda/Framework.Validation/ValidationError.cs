using FluentValidation.Results;
using HotChocolate;

namespace Framework.Validation
{
    public static class ValidationError
    {
        public static IError[] Create(ValidationResult result)
        {
            IError[] errors = new IError[result.Errors.Count];
            int index = 0;
            foreach(var item in result.Errors)
            {
                string propName = item.PropertyName;
                var error = ErrorBuilder.New()
                    .SetMessage(item.ErrorMessage)
                    .SetCode(item.PropertyName)
                    .SetCode(Char.ToLowerInvariant(propName[0]) + propName.Substring(1))
                    .Build();
                errors[index++] = error;
            }
            return errors;
        }
    }
}