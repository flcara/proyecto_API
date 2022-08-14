using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SoldProductsController:ControllerBase
    {
        [HttpGet(Name = "GetUsers")]
        public List<SoldProducts> GetSoldProducts()
        {
            return SoldProductsHandler.GetSoldProducts();
        }
    }
}
