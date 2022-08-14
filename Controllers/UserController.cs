using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase 
    {
        [HttpGet(Name = "GetUsers")]
        public List<Users> GetUsers()
        {
            return ProductHandler.GetUsers();
        }
        
    }
}
