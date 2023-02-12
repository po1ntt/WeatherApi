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
        public async Task<string> AddUserAsync(User user)
        {
            db.users.Add(new User
            {

                userName = user.userName,
                userPassword = user.userPassword
            });
            await db.SaveChangesAsync();

            return "Новый пользователь добавлен";
        }
        [HttpGet("LoginUser")]
        public async Task<User> LoginUser(string username, string password)
        {
           var user = await db.users.FirstOrDefaultAsync(p=> p.userName == username && p.userPassword == password);
           return user;
        }
        [HttpPost("AddFavoriteTownToUser")]
        public async Task<string> AddTownToUser(FavoriteTowns favoriteTowns)
        {
            bool result = await CheckExist(favoriteTowns.townId, favoriteTowns.userId);
            var town = await db.town.FirstAsync(p => p.id_town == favoriteTowns.townId);

            if (result == false)
            {
                db.favoriteTowns.Add(new FavoriteTowns
                {
                    userId = favoriteTowns.userId,
                    townId = favoriteTowns.townId
                });

                await db.SaveChangesAsync();
                return "plus";
            }
            else
            {
                bool boolresult = await DeleteFavTown(favoriteTowns);
                if(boolresult == true)
                {
                    return "minus";
                }
                else
                {
                    return null;

                }
            }
       
        }
        [HttpGet("GetFavTowns")]
        public async Task<List<Towns>> GetFavoritesTowns(int id_user)
        {
            List<Towns> listtown = new List<Towns>();
            var listfav = await db.favoriteTowns.Where(p => p.userId == id_user).ToListAsync();
            
            foreach(var item in listfav)
            {
                var towninfavorite = await db.town.FirstOrDefaultAsync(p => p.id_town == item.townId);
                listtown.Add(towninfavorite);
            }
            return listtown;
        }
        [HttpDelete("DeleteFavTown")]
        public async Task<bool> DeleteFavTown(FavoriteTowns towns)
        {
            var favtodelete = db.favoriteTowns.FirstOrDefault(p=> p.userId == towns.userId && p.townId == towns.townId);
            if(favtodelete != null)
            {
                db.favoriteTowns.Remove(favtodelete);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet("CheckExistFavorite")]
        public async Task<bool> CheckExist(int townid, int user_id)
        {
            bool result = false;
            var favtown = await db.favoriteTowns.FirstOrDefaultAsync(p => p.townId == townid && p.userId == user_id);
           if(favtown != null)
            {
                result = true;
            }
            return result;
        }
    }
}
