using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopClient.Services
{
    public interface IProductService
    {
        Action? ProductAtion { get; set; }
        Task<ServiceResponse> AddProduct(Product model);
        Task GetAllProducts(bool featuredProducts);
        List<Product> AllProducts { get; set; }
        List<Product> FeaturedProducts { get; set; }

    }
}