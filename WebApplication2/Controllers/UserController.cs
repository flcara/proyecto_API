using Microsoft.AspNetCore.Mvc;
using WebApplication2.Controllers.DTOS;
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
            return UserHandler.GetUsers();
        }
        

        [HttpDelete]
        public bool DeleteUser(int id)
        {
            try
            {
                return UserHandler.DeleteUser(id);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo completar la operación\n" +
                    $"{ex.Message}");
                
                return false;
            }
        }
        [HttpPost]
        public bool AddUser([FromBody] PostUser postUser)
        {
            try
            {
                return UserHandler.AddUser(new Users
                {
                    name = postUser.postUserName,
                    userLastName = postUser.postUserLastName,
                    userName = postUser.postUserCodeName,
                    password = postUser.postUserPassword,
                    mail = postUser.postUserMail,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar usuario\n" +
                    $"{ex.Message}");
                return false;               
            }          
        }
        [HttpPut]
        public bool ModifyUser([FromBody] PutUser putUser)
        {
            return UserHandler.ModifyUser(new Users { 
            
            userId=putUser.Id,
            name = putUser.putUserName,
            userLastName = putUser.putUserLastName,
            userName = putUser.putUserCodeName,
            mail = putUser.putUserMail,
            password=putUser.putUserPassword           
            });
        }
        [HttpGet("SearchUsers")]
        public List<Users> SearchUsers(string name)
        {
         return UserHandler.SearchUser(name);
            
        }
    }
    
}
