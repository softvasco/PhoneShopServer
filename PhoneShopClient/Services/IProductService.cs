using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopClient.Services
{
    public interface IProductService
    {
        Action? ProductAction { get; set; }
        Task<ServiceResponse> AddProduct(Product model);
        Task GetAllProducts(bool featuredProducts);
        List<Product> AllProducts { get; set; }
        List<Product> FeaturedProducts { get; set; }
        List<Product> ProductsByCategory { get; set; }
        Task GetProductsByCategory(int categoryId);

    }
}