using Microsoft.EntityFrameworkCore;
using PhoneShopServer.Data;
using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopServer.Repositories
{
    public class CategoryRepository(AppDbContext appDbContext) : ICategory
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<ServiceResponse> AddCategory(Category model)
        {
            if (model is null) return new ServiceResponse(false, "Model is null");
            var (flag, message) = await CheckName(model.Name!);
            if (flag)
            {
                appDbContext.Categories.Add(model);
                await Commit();
                return new ServiceResponse(true, "Category saved");
            }
            return new ServiceResponse(flag, message);
        }

        public async Task<List<Category>> GetAllCategories() => await appDbContext.Categories.ToListAsync();
        
        private async Task<ServiceResponse> CheckName(string name)
        {
            var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Name!.ToLower()!.Equals(name.ToLower()));
            return category is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Category already exists");
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}