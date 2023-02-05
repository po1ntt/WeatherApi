using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApi.Model
{

    public class FavoriteTowns
    {
        [Key]
        public int idFavtowns { get; set; }
        [ForeignKey("userId")]
        public int userId { get; set; }
        [ForeignKey("townId")]
        public int townId{ get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Towns? Towns { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public User? user { get; set; }


    }
}
