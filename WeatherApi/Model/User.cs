using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Model
{
    public class User
    {

        public User()
        {
            this.FavoriteTowns = new HashSet<FavoriteTowns>();
        }
       
        [Key]
        public int id { get; set; }
        public string? userName { get; set; }
        public string? userPassword { get; set; }
        public ICollection<FavoriteTowns> FavoriteTowns { get; set; }
    }
}


