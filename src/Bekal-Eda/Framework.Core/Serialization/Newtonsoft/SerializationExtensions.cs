using Newtonsoft.Json;

namespace Framework.Core.Serialization.Newtonsoft
{
    public static class SerializationExtensions
    {
        public static JsonSerializerSettings WithNonDefaultConstructorContractResolver(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new NonDefaultConstructorContractResolver();
            return settings;
        }

        /// <summary>
        /// Deserialize object from json with JsonNet
        /// </summary>
        /// <typeparam name="T">Type of the deserialized object</typeparam>
        /// <param name="json">json string</param>
        /// <param name="type">object type</param>
        /// <returns>deserialized object</returns>
        public static object? FromJson(this string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type,
                new JsonSerializerSettings().WithNonDefaultConstructorContractResolver());
        }

        /// <summary>
        /// Serialize object to json with JsonNet
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>json string</returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings().WithNonDefaultConstructorContractResolver());
        }
    }
}
