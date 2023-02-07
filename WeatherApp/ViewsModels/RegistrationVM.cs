using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Service;
using WeatherApp.Views;

namespace WeatherApp.ViewsModels
{
    internal class RegistrationVM : INotifyPropertyChanged
    {
        RestDataService restDataService = new RestDataService();
        public ICommand RegistrationCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public RegistrationVM()
        {
            RegistrationCommand = new Command(async () =>
            {
                Users users = new Users();
                users.userPassword = Password;
                users.userName = Login;
                bool result = await restDataService.RegistrationUser(users);
                if (result)
                {
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert(";(", "wrong", "Закрыть");
                }

            });

            BackCommand = new Command(async () =>
            {

                await Shell.Current.Navigation.PopAsync();


            });
        }
        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged();
                }

            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }

            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
