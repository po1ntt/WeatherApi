
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewsModels
{
    public class TownInfoVm : BaseVm
    {
      
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            Labeler = value => new DateTime((long) value).ToString("H:mm"),
            LabelsRotation = 0,
            TextSize= 30,
            NameTextSize= 30,
            Name = "Время",
            
            // when using a date time type, let the library know your unit 
            UnitWidth = TimeSpan.FromHours(1).Ticks, 

            // if the difference between our points is in hours then we would:
            // UnitWidth = TimeSpan.FromHours(1).Ticks,

            // since all the months and years have a different number of days
            // we can use the average, it would not cause any visible error in the user interface
            // Months: TimeSpan.FromDays(30.4375).Ticks
            // Years: TimeSpan.FromDays(365.25).Ticks

            // The MinStep property forces the separator to be greater than 1 day.
            MinStep = TimeSpan.FromHours(1).Ticks
        }
    };
        public Axis[] YAxes { get; set; } =
       {
        new Axis
        {
           
            TextSize= 30,
            NameTextSize= 30,
            Name = "Температуры с",
            // when using a date time type, let the library know your unit 
            
        }
    };
        public ObservableCollection<DateTimePoint> Temperatyre_2m { get; set; }
        private ObservableCollection<Daily> _ListDailyData;

        public ObservableCollection<Daily> ListDailyData
        {
            get => _ListDailyData;
            set
            {
                if (_ListDailyData != value)
                {
                    _ListDailyData = value;
                    OnPropertyChanged();
                }
            }
        }
        private Daily _DailyData;

        public Daily DailyData
        {
            get => _DailyData;
            set
            {
                    _DailyData = value;
                    OnPropertyChanged(); 
       
            }
        }
        public Towns TownInfo { get; set; }
        public Weather WeatherInfo { get; set; }


        public TownInfoVm(Weather weather, Towns town)
        {
            ListDailyData = new ObservableCollection<Daily>();
            TownInfo = town;
            WeatherInfo = weather;
            Temperatyre_2m = new ObservableCollection<DateTimePoint>();
            Service.WeatherConvertToDaily weatherConvertToDaily = new Service.WeatherConvertToDaily();
            List<Daily> data = weatherConvertToDaily.ReturnDaily(weather);
            foreach (var item in data)
            {
                ListDailyData.Add(item);
            }
            DailyData = ListDailyData.FirstOrDefault();
            foreach(var item in DailyData.temperatyre)
            {
                Temperatyre_2m.Add(new DateTimePoint
                {
                    DateTime = item.time,
                    Value = item.temperature_2m
                    

                });
            }
            Series = new ISeries[]
                {
                 new LineSeries<DateTimePoint>
                {
                  TooltipLabelFormatter = (chartPoint) =>
                $"{new DateTime((long) chartPoint.SecondaryValue):h:mm}: {chartPoint.PrimaryValue}",
                Values = Temperatyre_2m,
                Fill = null
                }
                };

        }

        
        }
       

}



