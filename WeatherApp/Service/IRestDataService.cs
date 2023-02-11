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
        Task<string> AddFavoriteTown(FavoriteTowns favoriteTowns);
        Task<Towns> AddTown(string nametown1);
        Task<Users> AuthorizationUser(string username, string password);
        Task<List<Towns>> GetFavTowns();
        Task<bool> checkIsFavoriteTown(Towns towns);
        Task<Weather> GetWeatherInfoByTown(int id_town);
        Task<bool> RegistrationUser(Users users);
        Task<List<Towns>> GetAllTowns();
    }
}
