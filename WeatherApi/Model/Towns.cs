using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WeatherApi.Model
{
    public class Towns
    {
        [Key]
        public int id_town { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? number { get; set; }
        public string? postal_code { get; set; }
        public string? street { get; set; }
        public int confidence { get; set; }
        public string? region { get; set; }
        public string? region_code { get; set; }
        public string? county { get; set; }
        public string? locality { get; set; }
        public string? administrative_area { get; set; }
        public string? neighbourhood { get; set; }
        public string? country { get; set; }
        public string? country_code { get; set; }
        public string? continent { get; set; }
        public string? label { get; set; }
        public string? map_url { get; set; }
        [ForeignKey("Weather")]
        public Nullable<int> weather_id { get; set; }

    }
    public class Root { 
        public List<Towns?>? data { get; set; }
    }
   
}
