using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Service;
using WeatherApp.Views;

namespace WeatherApp.ViewsModels
{
    public partial class HomeVM : BaseVm
    {
        private readonly Task initTask;

        public ICommand AddOrRemoveFavorites { get; set; }
        public ICommand GoToWeather { get; set; }
        public ICommand SearchCommand { get; set; }




        private ObservableCollection<Towns> _FavoritesTownList;

        public ObservableCollection<Towns> FavoritesTownList
        {
            get => _FavoritesTownList;
            set
            { 
                if(_FavoritesTownList != value)
                {
                    _FavoritesTownList = value;
                    OnPropertyChanged();
                }

            
            }
        }
        



        private ObservableCollection<Towns> _TownsList;

        public ObservableCollection<Towns> TownsList
        {
            get => _TownsList;
            set
            {
                if (_TownsList != value)
                {
                    _TownsList = value;
                    OnPropertyChanged();
                }
             }
        }


        public RestDataService restDataserv = new RestDataService();

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

        public HomeVM()
        {
            
            FavoritesTownList = new ObservableCollection<Towns>();
            TownsList = new ObservableCollection<Towns>();
            SearchCommand = new Command(async () =>
            {
                if (IsBusy)
                    return;
                try
                {
                    IsBusy = true;
                    IsRunningForButton = true;
                      RestDataService restData = new RestDataService();
                    Towns towns = await restData.AddTown(SearchInfo);
                    if(towns.id_town != 0)
                    {
                        Weather weather = await restData.GetWeatherInfoByTown(towns.id_town);
                        if (weather != null)
                        {
                            await Task.Delay(1000);
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
                catch(Exception e)
                {
                    await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                }
                finally
                {
                    IsRunningForButton = false;

                    IsBusy = false;
                }
            });
            AddOrRemoveFavorites = new Command(async (object? args) =>
            {
                if (args is Towns towns)
                {
                    if (IsBusy)
                        return;
                    try
                    {
                        IsBusy = true; IsRunningForListTowns = true;
                        RestDataService restdata = new RestDataService();
                        FavoriteTowns favoriteTowns = new FavoriteTowns();
                        favoriteTowns.townId = towns.id_town;
                        favoriteTowns.userId = AuthorizationVM.UserInfo.id;
                        string result = await restdata.AddFavoriteTown(favoriteTowns);
                        await Task.Delay(600);
                        List<Towns> listfav = await restdata.GetFavTowns(AuthorizationVM.UserInfo);
                        if (FavoritesTownList.Count != 0)
                        {
                            FavoritesTownList.Clear();

                        }
                        foreach (var item in listfav)
                        {

                            FavoritesTownList.Add(item);
                        }
                        var alltow = await restdata.GetAllTowns();
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
                    catch(Exception e)
                    {
                        await Shell.Current.DisplayAlert("Ошибка",e.Message,"Ок");
                    }
                    finally
                    {
                        IsRunningForListTowns = false;

                        IsBusy = false;
                    }
                   
                }
            });
            this.initTask = InitAsync();

        }
        private async Task InitAsync()
        {
            if (IsBusy)
                return;
            try
            {
                RestDataService restData = new RestDataService();
                IsBusy = true;
                List<Towns> listfav = await restData.GetFavTowns(AuthorizationVM.UserInfo);
                if (FavoritesTownList.Count != 0)
                {
                    FavoritesTownList.Clear();
                    
                }
                foreach (var item in listfav)
                {
                   
                    FavoritesTownList.Add(item);
                }
                var alltow = await restData.GetAllTowns();
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
      

    }
}
