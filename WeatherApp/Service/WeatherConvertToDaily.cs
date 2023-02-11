using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class WeatherConvertToDaily
    {
        public List<Daily> ReturnDaily(Weather weather)
        {
            List<Daily> listDaily = new List<Daily>();

            var dateDayList = weather.dateDay.Split(' ');
            int countday = dateDayList.Count();
            var sunset = weather.sunset.Split(' ');
            var temperatyre_2m = weather.temperatyre2M.Split(' ');
            var temperatyre_min = weather.temperatyre_2m_min.Split(' ');
            var temperatyre_max = weather.temperatyre_2m_max.Split(' ');
            var sunrise = weather.sunrise.Split(' ');
            var time = weather.time.Split(' ');
            var relativehimidity_2m = weather.relativehimidity_2m.Split(' ');
            for( int i = 1 ; i <= countday ; i++  )
            {
                Daily daily = new Daily();
                daily.Id = i;
                daily.dateDay = Convert.ToDateTime(dateDayList[0]);
                dateDayList = dateDayList.Skip(1).ToArray();
                daily.sunset = Convert.ToDateTime(sunset[0]);
                sunset = sunset.Skip(1).ToArray();
                daily.sunrise = Convert.ToDateTime(sunrise[0]);
                sunrise = sunrise.Skip(1).ToArray();
                daily.temperature_2m_max = temperatyre_max[0];
                temperatyre_max = temperatyre_max.Skip(1).ToArray();
                daily.temperature_2m_min = temperatyre_min[0];
                temperatyre_min = temperatyre_min.Skip(1).ToArray();
                for(int u = 0; u <= 23; u++)
                {
                    daily.temperatyre.Add(new Temperatyre {
                        time = Convert.ToDateTime(time[u]),
                        temperature_2m = Convert.ToDouble(temperatyre_2m[u].Replace(',', '.'))

                    });
                   
                }
                time = time.Skip(24).ToArray();
                temperatyre_2m = temperatyre_2m.Skip(24).ToArray();
                relativehimidity_2m = relativehimidity_2m.Skip(24).ToArray();
                listDaily.Add(daily);

            }
            return listDaily;
        }
    }
}
