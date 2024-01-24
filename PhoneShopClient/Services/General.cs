using System.Text.Json.Serialization;
using System.Text.Json;

namespace PhoneShopClient.Services
{
    public static class General
    {
        public static string SerializeObj(object modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
        public static StringContent GenerateStringContent(string serializedObj) => new(serializedObj, System.Text.Encoding.UTF8, "application/json");
        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
        public static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }

        public static string GetDescription(string description)
        {
            string appendDots = "...";
            int maxLenth = 100;
            int descriptionLength = description.Length;
            return descriptionLength > maxLenth ? $"{description.Substring(0, 100)}{appendDots}" : description;
        }
    }
}