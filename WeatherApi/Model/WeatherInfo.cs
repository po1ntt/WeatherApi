using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WeatherApi.Model
{
    public class WeatherInfo
    {
        [Key]
        public int idWeather { get; set; }

        [ForeignKey("townId")]
        public int townsId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Towns Towns { get; set; }
        public string dateDay { get; set; }

        public string time { get; set; }
        public string temperatyre2M { get; set; }
        public string temperatyre_2m_max { get; set; }
        public string temperatyre_2m_min { get; set; }
        public string relativehimidity_2m { get; set;  }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string precipitation { get; set; }

        public DateTime updateDate { get; set; }
    }
}
