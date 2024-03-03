using Newtonsoft.Json.Serialization;

namespace Framework.Core.Serialization.Newtonsoft
{
    public class NonDefaultConstructorContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            return JsonObjectContractProvider.UsingNonDefaultConstructor(
                base.CreateObjectContract(objectType),
                objectType,
                base.CreateConstructorParameters
            );
        }
    }
}
