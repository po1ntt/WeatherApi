

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewsModels;

namespace WeatherApp.Service
{
    
    public class RestDataService
    {
        public HttpClient httpclient;
        private readonly string Adress;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public RestDataService()
        {
            httpclient = new HttpClient();
            Adress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5198" : "https://localhost:7198";
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<string> AddFavoriteTown(FavoriteTowns favoriteTowns)
        {
            string result = "ghj";
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return result;
            }
            try
            {
               
                var json = JsonSerializer.Serialize(favoriteTowns);
                StringContent favtowns = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync($"{Adress}/Users/AddFavoriteTownToUser", favtowns);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
            return result;
        }

        public async Task<Towns> AddTown(string nametown1)
        {
            Towns towns = new Towns();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return towns;
            }
            try
            {
                var json = JsonSerializer.Serialize(nametown1);
                StringContent nametown = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync($"{Adress}/Weather/addnewtown", nametown);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    towns = JsonSerializer.Deserialize<Towns>(data, jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return towns;
            }
            return towns;
        }

        public async Task<Users> AuthorizationUser(string username, string password)
        {
            Users result = new Users();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return result;
            }
            try
            {
                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Users/LoginUser?username={username}&password={password}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<Users>(data, jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
            return result;
        }

        public async Task<Towns> DeleteTownFromFavorite(int id_town)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Towns>> GetAllTowns()
        {
            List<Towns> towns = new List<Towns>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return towns;
            }
            try
            {

                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Towns/GetTowns");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    towns = JsonSerializer.Deserialize<List<Towns>>(data, jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return towns;
            }
            return towns;
        }
        public async Task<List<Towns>> GetFavTowns(Users users)
        {
            List<Towns> towns = new List<Towns>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return towns;
            }
            try
            {
                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Users/GetFavTowns?id_user={users.id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    towns = JsonSerializer.Deserialize<List<Towns>>(data, jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return towns;
            }
            return towns;
        }

        public async Task<bool> checkIsFavoriteTown(Towns towns)
        {
            bool result = false;
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return result;
            }
            try
            {
               
                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Users/CheckExistFavorite?townid={towns.id_town}&user_id=2");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<bool>(data, jsonSerializerOptions);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
            return result;
        }

        public async Task<Weather> GetWeatherInfoByTown(int id_town)
        {
            Weather weather = new Weather();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return weather;
            }
            try
            {

                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Weather/ReturnWeatherByTown?id_town={id_town}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    weather = JsonSerializer.Deserialize<Weather>(data);
                }
                else
                {
                    Console.WriteLine("202");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return weather;
            }
            return weather;
        }

        public async Task<bool> RegistrationUser(Users users)
        {
            throw new NotImplementedException();
        }
    }
}
