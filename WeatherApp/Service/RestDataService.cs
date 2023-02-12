

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
    
    public class RestDataService : IRestDataService
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
                await Shell.Current.DisplayAlert("Ошибка", "No Internet", "Ок");
                return result;
            }
            try
            {
               
                var json = JsonSerializer.Serialize(favoriteTowns);
                StringContent favtowns = new(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync($"{Adress}/Users/AddFavoriteTownToUser", favtowns);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Badrequest", "Ок");
                    return result;

                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                return result;
            }
            return result;
        }

        public async Task<Towns> AddTown(string nametown1)
        {
            Towns towns = new ();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return towns;
            }
            try
            {
                var json = JsonSerializer.Serialize(nametown1);
                StringContent nametown = new(json, Encoding.UTF8, "application/json");
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
            Users result = new ();
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

    
        public async Task<List<Towns>> GetAllTowns()
        {
            List<Towns> towns = new();
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
        public async Task<List<Towns>> GetFavTowns()
        {
            List<Towns> towns = new();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return towns;
            }
            try
            {
                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Users/GetFavTowns?id_user={Preferences.Default.Get("id_user", 0)}");
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
               
                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Users/CheckExistFavorite?townid={towns.id_town}&user_id={Preferences.Default.Get("id_user",0)}");
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
            Weather weather = new();
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
        public async Task<Weather> GetWeatherInfoByTownInInterval(double latitude, double longtitude, DateTime startdate, DateTime enddate)
        {
            Weather weather = new();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("504");
                return weather;
            }
            try
            {

                HttpResponseMessage response = await httpclient.GetAsync($"{Adress}/Weather/GetWetherInInterval?latitude={latitude}&longtitude={longtitude}&date1={startdate}&date2={enddate}");
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

        public async Task<string> RegistrationUser(Users users)
        {   
            string result = "";
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Ошибка", "No Internet", "Ок");
                return result;
            }
            try
            {

                var json = JsonSerializer.Serialize(users);
                StringContent user = new(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync($"{Adress}/Users/AddUser", user);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Badrequest", "Ок");
                    return result;

                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                return result;
            }
            return result;
        }
    }
}
