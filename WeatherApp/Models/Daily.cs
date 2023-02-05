using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Daily
    {
        public int Id { get; set; }
        public DateTime dateDay { get; set; }
        public double temperature_2m_max { get; set; }
        public double temperature_2m_min { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public List<DateTime> times { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<int> relativehumidity_2m { get; set; }
        public List<double> windspeed_10m { get; set; }

    }
}
