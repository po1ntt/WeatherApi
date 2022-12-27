using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Model
{
    public class Towns
    {
        [Key]
        public int id_town { get; set; }
        public  string? NameTown { get; set; }
    }
}
