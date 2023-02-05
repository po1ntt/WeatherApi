using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Towns
    {
        public int id_town { get; set; }
        public string? name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? locality { get; set; }
        public string? country { get; set; }
        public string? continent { get; set; }
    }
}
