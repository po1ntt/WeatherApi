using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Daily
    {
        public Daily()
        {
            this.temperatyre= new HashSet<Temperatyre>();
        }
        public int Id { get; set; }
        public DateTime dateDay { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
       
        public ICollection<Temperatyre> temperatyre { get; set;}

    }
    public class Temperatyre
    {
        public double temperature_2m { get; set;}
        public DateTime time { get; set; }
        public int relativehumidity_2m { get; set; }


    }

}
