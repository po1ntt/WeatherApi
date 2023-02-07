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
    public class AuthorizationVM : BaseVm
    {


        RestDataService restDataService = new RestDataService();
        public static Users UserInfo { get; set; }
		public ICommand AutorizeUser { get; set; }
        public ICommand NavToReg { get; set; }
		
		public AuthorizationVM()
		{
            AutorizeUser = new Command(async () =>
			{
                if (IsBusy)
                    return;
                try
                {
                    IsBusy = true;
                    if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                    {
                        await Shell.Current.DisplayAlert(";(", "Логин или пароль введен неправильно", "Закрыть");
                    }
                    else
                    {

                        UserInfo = await restDataService.AuthorizationUser(Login, Password);
                        if (UserInfo != null)
                        {
                            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert(";(", "Логин или пароль введен неправильно", "Закрыть");
                        }

                    }
                }
                catch(Exception e)
                {
                    await Shell.Current.DisplayAlert("Ошибка", e.Message, "Ок");
                }
                finally
                {
                    IsBusy = false;
                }
				
               

			});

			NavToReg = new Command(async () =>
			{

				await Shell.Current.Navigation.PushModalAsync(new RegistrationPage());


			});
        }


        private string _login;

        public string Login
        {
            get => _login;
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
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }

            }
        }
       



    }
}
