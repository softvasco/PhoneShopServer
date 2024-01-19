using PhoneShopSharedLibrary.Contracts;
using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhoneShopClient.Services
{
    public class ClientServices(HttpClient httpClient) : IProduct
    {
        private const string BaseUrl = "api/product";
        private static string SerializeObj(object modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
        private static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
        private static StringContent GenerateStringContent(string serializedObj) => new(serializedObj, System.Text.Encoding.UTF8, "application/json");
        private static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
        private static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }

        public async Task<ServiceResponse> AddProduct(Product model)
        {
            var response = await httpClient.PostAsync(BaseUrl, GenerateStringContent(SerializeObj(model)));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse(false, "Error occured, Try again later...");
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            return DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            var response = await httpClient.GetAsync($"{BaseUrl}?featured={featuredProducts}");
            if (!response.IsSuccessStatusCode)
            {
                return null!;
            }

            var result = await response.Content.ReadAsStringAsync();
            return [.. DeserializeJsonStringList<Product>(result)];
        }
    }
}