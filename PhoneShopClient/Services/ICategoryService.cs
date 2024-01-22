using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopClient.Services
{
    public interface ICategoryService
    {
        Action? CategoryAction { get; set; }
        Task<ServiceResponse> AddCategory(Category model);
        Task GetAllCategories();
        List<Category> AllCategories { get; set; }
    }
}