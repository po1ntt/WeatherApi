using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Service
{
    
    internal class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpclient;
        private readonly string _BaseAdress;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpclient = new HttpClient();
            _BaseAdress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5198" : "https://localhost:7198";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public Task AddFavoriteTown(int id_town, string username)
        {
            throw new NotImplementedException();
        }

        public Task AddTown(string nametown)
        {
            throw new NotImplementedException();
        }

        public Task AuthorizationUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTownFromFavorite(int id_town)
        {
            throw new NotImplementedException();
        }

        public Task GetWeatherInfoByTown(int id_town)
        {
            throw new NotImplementedException();
        }

        public Task RegistrationUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
