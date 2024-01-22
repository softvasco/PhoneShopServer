using Microsoft.AspNetCore.Mvc;
using PhoneShopServer.Repositories;
using PhoneShopSharedLibrary.Models;
using PhoneShopSharedLibrary.Responses;

namespace PhoneShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategory categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllCategories()
        {
            var products = await categoryService.GetAllCategories();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddCategory(Category model)
        {
            if (model is null)
                return BadRequest("Model is null");

            var response = await categoryService.AddCategory(model);

            return Ok(response);
        }
    }
}
