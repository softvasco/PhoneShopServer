using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopClient.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse> AddCategory(Category model);
        Task<List<Category>> GetAllCategories();

    }
}