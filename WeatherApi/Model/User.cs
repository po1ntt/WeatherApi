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
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }


        public virtual ICollection<FavoriteTowns>? FavoriteTowns { get; set; }
    }
}
