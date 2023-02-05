using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        #region HTTPGetUsers


        [HttpGet("GetUsers")]
        public IEnumerable<User> Get()
        {
            var collection = db.users.ToList();
            return collection;
        }

      
        #endregion
        [HttpPost("AddUser")]
        public async Task<string> AddUserAsync(string username, string password)
        {
            db.users.Add(new User
            {

                userName = username,
                userPassword = password
            });
            await db.SaveChangesAsync();

            return "Новый пользователь добавлен";
        }
        [HttpGet("LoginUser")]
        public async Task<User> LoginUser(string username, string password)
        {
           var user = db.users.FirstOrDefault(p=> p.userName == username && p.userPassword == password);
           return user;
        }
        [HttpPost("AddFavoriteTownToUser")]
        public async Task<bool> AddTownToUser(FavoriteTowns favoriteTowns)
        {
            var town = await db.town.FirstAsync(p => p.id_town == favoriteTowns.townId);
            db.favoriteTowns.Add(new FavoriteTowns {
                userId = favoriteTowns.userId,
                Towns = town
            });
           
            await db.SaveChangesAsync();
       
            return true;
        }
        [HttpGet("GetFavotesTowns")]
        public async Task<List<Towns>> GetFavoritesTowns(User user)
        {
            List<Towns> listtown = new List<Towns>();
            var listfav = db.favoriteTowns.ToList().Where(p => p.userId == user.id);
            foreach(var item in listfav)
            {
                var towninfavorite = await db.town.FirstAsync(p => p.id_town == item.townId);
                listtown.Add(towninfavorite);
            }
            return listtown;
        }
    }
}
