using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.Json;

using static System.Net.WebRequestMethods;
using static WeatherApi.Model.WeatherInfo;
using System.Runtime.Serialization;
using System.Globalization;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TownsController : Controller
    {
        ApplicationDbContext db;
        public TownsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet(Name = "GetTownInfoByName")]
        public async Task<Root>? Get(string nametown)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format($"http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query={nametown}&limit=1")).Result;
            result.EnsureSuccessStatusCode();
            if (result != null)
            {
                string responsebody = await result.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(responsebody);


                return data;

            }
            else
            {
                return null;
            }


        }



    }
}
