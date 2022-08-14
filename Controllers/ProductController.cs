using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        [HttpGet(Name = "GetProducts")]
        public List<Products> GetProducts()
        {
            return ProductHandler.GetProducts();
        }
    }
}
