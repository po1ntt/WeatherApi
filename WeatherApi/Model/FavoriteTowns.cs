using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Model
{
    public class FavoriteTowns
    {
        [Key]
        public int id_favoriteTown { get; set; }
        public User? User { get; set; }
        public Towns? Towns { get; set; }
    }
}
