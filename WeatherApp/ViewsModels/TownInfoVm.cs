
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using WeatherApp.Models;
using WeatherApp.Service;

namespace WeatherApp.ViewsModels
{
    public class TownInfoVm : BaseVm
    {
         public Command SelectionChanged { get; set; }
        public Command ChangeInterval { get; set; }

        public ISeries[] Series { get; set; }

        private System.Drawing.Color _ColorForTown;

        public System.Drawing.Color ColorForTown
        {
            get => _ColorForTown;
            set { _ColorForTown = value;
                OnPropertyChanged();
            }
        }

        private DateTime _StartDate;

        public DateTime StartDate
        {
            get => _StartDate;
            set { _StartDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _EndDate;

        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged();
            }
        }

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            Labeler = value => new DateTime((long) value).ToString("H:mm"),
            LabelsRotation = 0,
            TextSize= 30,
            NameTextSize= 30,
            Name = "Время",
            
            UnitWidth = TimeSpan.FromHours(1).Ticks, 

      
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
        private ObservableCollection<DateTimePoint> _Temperatyre_2m;

        public ObservableCollection<DateTimePoint> Temperatyre_2m
        {
            get => _Temperatyre_2m;
            set { _Temperatyre_2m = value;
                OnPropertyChanged();
            }
        }

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


        public RestDataService _RestDataService;

        public WeatherConvertToDaily _WeatherConvert;
        public TownInfoVm(Weather weather, Towns town)
        {
            _RestDataService = new RestDataService();
            _WeatherConvert = new WeatherConvertToDaily();
            ListDailyData = new ObservableCollection<Daily>();
            TownInfo = new Towns();
            WeatherInfo = new Weather();
            Temperatyre_2m = new ObservableCollection<DateTimePoint>();
            SelectionChanged = new Command( () => ChangeTemperatyre());
            ChangeInterval = new Command(async () => await UpdateIntervalDates(town));
            initAsync(weather, town);
           

        }
        public void ChangeTemperatyre()
        {
            Temperatyre_2m.Clear();
            foreach (var item in DailyData.temperatyre)
            {
                Temperatyre_2m.Add(new DateTimePoint
                {
                    DateTime = item.time,
                    Value = item.temperature_2m
                });
            }
        }
        public  void initAsync(Weather weather, Towns towns)
        {
            TownInfo = towns;
            WeatherInfo = weather;

          
            List<Daily> data = _WeatherConvert.ReturnDaily(weather);
            foreach (var item in data)
            {
                ListDailyData.Add(item);
            }
            DailyData = ListDailyData.FirstOrDefault();
            foreach (var item in DailyData.temperatyre)
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
                 
                        
                     
                  DataLabelsSize= 24,
                  GeometrySize= 10,
                Values = Temperatyre_2m,
                Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(40))

                }
               };
        }
        public async Task UpdateIntervalDates(Towns towns)
        {
            if (EndDate >= StartDate)
            {
                Weather weather = await _RestDataService.GetWeatherInfoByTownInInterval(towns.latitude, towns.longitude, StartDate, EndDate);
                List<Daily> data = _WeatherConvert.ReturnDaily(weather);
                ListDailyData.Clear();
                foreach (var item in data)
                {
                    ListDailyData.Add(item);
                }
                DailyData = ListDailyData.FirstOrDefault();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Конечная дата должна быть больше или равна начальной дате", "Ок");
            }
           
           
        }
        public async Task AddOrDeleteFavorite(Towns towns)
        {
            
                if (IsBusy)
                    return;
                try
                {
                    IsBusy = true; 
                    FavoriteTowns favoriteTowns = new FavoriteTowns();
                    favoriteTowns.townId = towns.id_town;
                    HomeVM homeVM = new HomeVM();
                    favoriteTowns.userId = Preferences.Default.Get("id_user", 0);
                    var result = await _RestDataService.AddFavoriteTown(favoriteTowns);
                    if(result == "plus")
                    {
                         ColorForTown = ColorTranslator.FromHtml("#141be0");
                        homeVM.FavoritesTownList.Add(towns);
                        homeVM.TownsList.Remove(towns);
                    }
                    else
                    {
                        ColorForTown = ColorTranslator.FromHtml("#373737");
                        homeVM.FavoritesTownList.Remove(towns);
                        homeVM.TownsList.Add(towns);


                    }
               
                }
                catch (Exception e)
                {
                    await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                }
                finally
                {
                    IsBusy = false;
                }

            
        }


    }
       

}



