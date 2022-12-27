using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    // http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query=Berlin&limit=1
    public class UsersController : Controller
    {
        ApplicationDbContext db;
        public UsersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet(Name ="GetUsers")]
        public IEnumerable<User> Get()
        {
            var collection =  db.Users.ToList();
            return collection;
        }
        [HttpGet("FindUser")]

        public User? FindUser(string username, string password)
        {   
           var user = db.Users.FirstOrDefault(p => p.UserName == username && p.UserPassword == password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        
        [HttpPost(Name ="AddUser")]
        public async Task<string> AddUserAsync(string username, string password)
        {
            db.Users.Add(new User
            {
                
                UserName = username,
                UserPassword = password
            });
            await db.SaveChangesAsync();
            
            return "Новый пользователь добавлен";
        }
    }
}
