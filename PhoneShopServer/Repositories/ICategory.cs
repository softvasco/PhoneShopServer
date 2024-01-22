

using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopServer.Repositories
{
    public interface ICategory
    {
        Task<ServiceResponse> AddCategory(Category model);
        Task<List<Category>> GetAllCategories();
    }
}