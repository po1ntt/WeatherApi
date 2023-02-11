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


        public static Users UserInfo { get; set; }
		public Command AutorizeUser { get; set; }
        public Command NavToReg { get; set; }
        public RestDataService _RestDataService;

        public AuthorizationVM()

		{
            _RestDataService = new RestDataService();
            AutorizeUser = new Command(async () => await AuthorizeAsync());
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
        public async Task AuthorizeAsync()
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

                    UserInfo = await _RestDataService.AuthorizationUser(Login, Password);
                    if (UserInfo.id != 0)
                    {
                        Preferences.Default.Set("id_user", UserInfo.id);

                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert(";(", "Логин или пароль введен неправильно", "Закрыть");
                    }

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
