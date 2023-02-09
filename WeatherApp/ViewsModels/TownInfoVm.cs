
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewsModels
{
    public partial class TownInfoVm : BaseVm
    {
        private readonly Task initTask;
      
        public ObservableCollection<ISeries> Series;

        public ObservableCollection<ObservableValue> Temperatyre_2m { get; set; }
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
            Temperatyre_2m = new ObservableCollection<ObservableValue>();
            Service.WeatherConvertToDaily weatherConvertToDaily = new Service.WeatherConvertToDaily();
            List<Daily> data = new List<Daily>();
            data = weatherConvertToDaily.ReturnDaily(weather);
            foreach (var item in data)
            {
                ListDailyData.Add(item);
            }
            DailyData = ListDailyData.FirstOrDefault();
            foreach(var item in DailyData.temperatyre)
            {
                Temperatyre_2m.Add(new ObservableValue
                {
                    Value = item.temperature_2m,

                });
            }
            Series = new ObservableCollection<ISeries>
                {
                 new LineSeries<ObservableValue>
                {
                Values = Temperatyre_2m,
                Fill = null
                }
                };

        }

        private async Task InitAsync(Weather weather)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
               
                



            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ex", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }

           
        }

        }
       

}



