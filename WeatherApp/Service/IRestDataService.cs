using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public interface IRestDataService
    {
        public Task<Towns> AddTown(string nametown);
        public Task<List<Towns>> GetAllTowns();
        public Task<bool> AddFavoriteTown(FavoriteTowns favoriteTowns);
        public Task<Towns> DeleteTownFromFavorite(int id_town);    
        public Task<Towns> GetWeatherInfoByTown(int id_town);
        public Task<Users> AuthorizationUser(string username, string password);
        public Task<bool> RegistrationUser(Users users);
       
    }
}
