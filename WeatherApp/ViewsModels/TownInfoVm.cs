
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherApp.Models;

namespace WeatherApp.ViewsModels
{
    public partial class TownInfoVm : BaseVm
    {
        private readonly Task initTask;

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
                if (_DailyData != value)
                {
                    _DailyData = value;
                    OnPropertyChanged(); 
                } 
            }
        }
        public Towns TownInfo { get; set; }
        public Weather WeatherInfo { get; set; }


        public TownInfoVm(Weather weather, Towns town)
        {
            ListDailyData = new ObservableCollection<Daily>();
            TownInfo = town;
            WeatherInfo = weather;
           
            this.initTask = InitAsync(weather);
            DailyData = ListDailyData.FirstOrDefault();
            
        }

        private async Task InitAsync(Weather weather)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Service.WeatherConvertToDaily weatherConvertToDaily = new Service.WeatherConvertToDaily();
                List<Daily> data = new List<Daily>();
                data = weatherConvertToDaily.ReturnDaily(weather);
                foreach(var item in data)
                {
                    ListDailyData.Add(item);
                }

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


