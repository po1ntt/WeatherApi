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
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? locality { get; set; }
        public string? country { get; set; }
        public string? continent { get; set; }
    }
    public class Root
    {
        public List<Towns?>? data { get; set; }
    }

}
