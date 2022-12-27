using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Model
{
    public class WeatherTown
    {
        [Key]
        public int id_weather { get; set; }
        public Towns? Towns { get; set; }
    }
}
