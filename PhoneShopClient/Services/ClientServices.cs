using Microsoft.AspNetCore.Http;
using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopClient.Services
{
    public class ClientServices(HttpClient httpClient) : IProductService, ICategoryService
    {
        private const string ProductBaseUrl = "api/product";
        private const string CategoryBaseUrl = "api/category";

        public Action? CategoryAction { get ; set ; }
        public List<Category> AllCategories { get; set; }
        public Action? ProductAtion { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Product> FeaturedProducts { get; set; }


        // Products

        public async Task<ServiceResponse> AddProduct(Product model)
        {
            var response = await httpClient.PostAsync(ProductBaseUrl, General.GenerateStringContent(General.SerializeObj(model)));

            var result = CheckResponse(response);
            if (!result.Flag)
                return result;

            var apiResponse = await ReadContent(response);
            var data = General.DeserializeJsonString<ServiceResponse>(apiResponse);
            if (!data.Flag)
            {
                return data;
            }
            await ClearAndGetAllProducts();
            return data;
        }

        private async Task ClearAndGetAllProducts()
        {
            bool featuredProduct = true;
            bool allProducts = false;
            AllProducts = null!;
            FeaturedProducts = null!;
            await GetAllProducts(featuredProduct);
            await GetAllProducts(allProducts);
        }

        public async Task GetAllProducts(bool featuredProducts)
        {
            if (featuredProducts && FeaturedProducts is null)
            {
                FeaturedProducts = await GetProducts(featuredProducts);
                ProductAtion?.Invoke();
                return;
            }

            if (!featuredProducts && AllProducts is null)
            {
                AllProducts = await GetProducts(featuredProducts);
                ProductAtion?.Invoke();
                return;
            }
        }

        private async Task<List<Product>> GetProducts(bool featured)
        {
            var response = await httpClient.GetAsync($"{ProductBaseUrl}?featured={featured}");
            var (flag, _) = CheckResponse(response);
            if (!flag) return null!;

            var result = await ReadContent(response);
            return (List<Product>?)General.DeserializeJsonStringList<Product>(result)!;
        }


        //Categories
        public async Task<ServiceResponse> AddCategory(Category model)
        {
            var response = await httpClient.PostAsync(CategoryBaseUrl, General.GenerateStringContent(General.SerializeObj(model)));

            var result = CheckResponse(response);
            if (!result.Flag)
                return result;

            var apiResponse = await ReadContent(response);
            var data = General.DeserializeJsonString<ServiceResponse>(apiResponse);
            if(!data.Flag)
            {
                return data;
            }
            await ClearAndGetAllCategories();
            return data;
        }

        public async Task GetAllCategories()
        {
            if (AllCategories is null)
            {
                var response = await httpClient.GetAsync($"{CategoryBaseUrl}");
                var (flag, _) = CheckResponse(response);
                if (!flag) return;

                var result = await ReadContent(response);
                AllCategories = (List<Category>?)General.DeserializeJsonStringList<Category>(result)!;
                CategoryAction?.Invoke();
            }
        }

        private async Task ClearAndGetAllCategories()
        {
            AllCategories = null!;
            await GetAllCategories();
        }


        //General method

        private static async Task<string> ReadContent(HttpResponseMessage response) => await response.Content.ReadAsStringAsync();

        private static ServiceResponse CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse(false, "Error occured, Try again later...");
            else
                return new ServiceResponse(true, null!);
        }


    }
}