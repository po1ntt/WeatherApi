using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApi.Model
{
    public class WeatherInfo
    {
        
        [Key]
        public int id_weather { get; set; }

        public Towns Towns { get; set; }
        public string time { get; set; }
        public string temperatyre_2m { get; set; }
           

    }
}
