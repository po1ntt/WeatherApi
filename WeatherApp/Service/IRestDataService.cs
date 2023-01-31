using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Service
{
    internal interface IRestDataService
    {
        public Task AddTown(string nametown);
        public Task AddFavoriteTown(int id_town, string username);
        public Task DeleteTownFromFavorite(int id_town);    
        public Task GetWeatherInfoByTown(int id_town);
        public Task AuthorizationUser(string username, string password);
        public Task RegistrationUser(string username, string password);
       
    }
}
