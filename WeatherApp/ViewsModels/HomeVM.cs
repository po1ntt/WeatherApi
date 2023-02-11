using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Service;
using WeatherApp.Views;

namespace WeatherApp.ViewsModels
{
    public partial class HomeVM : BaseVm
    {
        private Task initAsync;
        public Command AddOrRemoveFavorites { get; set; }
        public Command GoToWeather { get; set; }
        public Command SearchCommand { get; set; }
        public Command OutCommand { get; set; }
        public Command GoToAllTowns { get; set; }

        public Command RefreshFavorites { get; set; }

        private ObservableCollection<Towns> _FavoritesTownList;

        public ObservableCollection<Towns> FavoritesTownList
        {
            get => _FavoritesTownList;
            set
            { 
               
                    _FavoritesTownList = value;
                    OnPropertyChanged();
                

            
            }
        }
        



        private ObservableCollection<Towns> _TownsList;

        public ObservableCollection<Towns> TownsList
        {
            get => _TownsList;
            set
            {
             
                    _TownsList = value;
                    OnPropertyChanged();
                
             }
        }



        private string _SearchInfo;

        public string SearchInfo
        {
            get => _SearchInfo;
            set { 
                if(_SearchInfo != value)
                {
                    _SearchInfo = value;
                    if (!string.IsNullOrWhiteSpace(_SearchInfo))
                        IsEnabled = true;
                    else
                        IsEnabled = false;
                   
                    
                    OnPropertyChanged();
                }
               
            }
        }
        private bool _IsEnabled;

        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {

                if (_IsEnabled == value)
                    return;

                _IsEnabled = value;
                OnPropertyChanged();

            }
        }
        private bool _IsRunningForButton;

        public bool IsRunningForButton
        {
            get => _IsRunningForButton;
            set
            {

                if (_IsRunningForButton == value)
                    return;

                _IsRunningForButton = value;
                OnPropertyChanged();

            }
        }
        private bool _IsRunningForListTowns;

        public bool IsRunningForListTowns
        {
            get => _IsRunningForListTowns;
            set
            {

                if (_IsRunningForButton == value)
                    return;

                _IsRunningForButton = value;
                OnPropertyChanged();

            }
        }
        public RestDataService _RestDataService;

        public HomeVM()
        {
            _RestDataService = new RestDataService();
            FavoritesTownList = new ObservableCollection<Towns>();
            TownsList = new ObservableCollection<Towns>();
            SearchCommand = new Command(async () => await FindWeather());
            GoToWeather = new Command(async (object args) => await FindWeather(args));
            GoToAllTowns = new Command(async () => await Shell.Current.Navigation.PushAsync(new AllTowns()));
            RefreshFavorites = new Command(async () => await RefreshFavoritesTask());

            AddOrRemoveFavorites = new Command(async (object? args) => await AddOrDeleteFavorite(args));
            initAsync = InitAsync();

        }
        public async Task InitAsync()
        {
            if (IsBusy)
                return;
            try
            {
           
                IsBusy = true;
                List<Towns> listfav = await _RestDataService.GetFavTowns();
                if (FavoritesTownList.Count != 0)
                {
                    FavoritesTownList.Clear();
                    
                }
                foreach (var item in listfav)
                {
                   
                    FavoritesTownList.Add(item);
                }
                var alltow = await _RestDataService.GetAllTowns();
                if (TownsList.Count != 0)
                {
                    TownsList.Clear();
                }
                foreach (var item in alltow)
                {

                    
                    var town = listfav.FirstOrDefault(p => p.id_town == item.id_town);
                    if (town != null)
                    {
                        continue;
                    }
                    else
                    {
                        TownsList.Add(item);


                    }

                }
              
            }
           
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("ex", ex.Message, "ok");

            }
            finally
            {
                IsBusy = false;        
            }

        }
        public async Task AddOrDeleteFavorite(object args)
        {
            if (args is Towns towns)
            {
                if (IsBusy)
                    return;
                try
                {
                    IsBusy = true; IsRunningForListTowns = true;
                    FavoriteTowns favoriteTowns = new FavoriteTowns();
                    favoriteTowns.townId = towns.id_town;
                    favoriteTowns.userId = Preferences.Default.Get("id_user", 0);
                    var result = await _RestDataService.AddFavoriteTown(favoriteTowns);
                    if (result == "plus")
                    {
                       
                        TownsList.Remove(towns);
                    }
                    else
                    {
                        FavoritesTownList.Remove(towns);
                        TownsList.Add(towns);
                    }
                }
                catch (Exception e)
                {
                    await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                }
                finally
                {
                    IsRunningForListTowns = false;
                    IsBusy = false;
                }

            }
        }
        public async Task FindWeather()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                IsRunningForButton = true;
               
                Towns towns = await _RestDataService.AddTown(SearchInfo);
                if (towns.id_town != 0)
                {
                    if (!FavoritesTownList.Contains(towns))
                    {
                        if (!TownsList.Contains(towns))
                        {
                            TownsList.Add(towns);
                        }
                    }
                    Weather weather = await _RestDataService.GetWeatherInfoByTown(towns.id_town);
                    if (weather != null)
                    {
                        await Shell.Current.Navigation.PushAsync(new TownInfoPage(weather, towns));
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Ошибка", "Погода не найдена", "Ок");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Город не найден", "Ок");
                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
            }
            finally
            {
                IsRunningForButton = false;
                IsBusy = false;
            }
        }
        public async Task FindWeather(object args)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                IsRunningForButton = true;
                if(args is Towns towns)
                {
                    if (towns.id_town != 0)
                    {
                        Weather weather = await _RestDataService.GetWeatherInfoByTown(towns.id_town);
                        if (weather != null)
                        {
                            await Shell.Current.Navigation.PushAsync(new TownInfoPage(weather, towns));
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Ошибка", "Погода не найдена", "Ок");
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Ошибка", "Город не найден", "Ок");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Город не найден", "Ок");

                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
            }
            finally
            {
                IsRunningForButton = false;
                IsBusy = false;
            }
        }
        public async Task RefreshFavoritesTask()
        {
            var favorites = await _RestDataService.GetFavTowns();
            if (FavoritesTownList.Count() != 0)
                FavoritesTownList.Clear();
            foreach(var item in favorites)
            {
                FavoritesTownList.Add(item);
            }
        }

    }
}
