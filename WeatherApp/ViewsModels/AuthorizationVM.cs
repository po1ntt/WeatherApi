using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Service;
using WeatherApp.Views;

namespace WeatherApp.ViewsModels
{
    internal class AuthorizationVM : BaseVm
    {
        private readonly IRestDataService _dataservice;

        public static Users UserInfo { get; set; }
		public ICommand AutorizeUser { get; set; }
		public AuthorizationVM(IRestDataService dataService)
		{
			_dataservice = dataService;
			AutorizeUser = new Command(async () =>
			{
				UserInfo = await _dataservice.AuthorizationUser(Login, Password);
				if(UserInfo != null)
				{
					await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
				}
				else
				{

				}

			}, () => Login.Length > 5 && Password.Length > 8);

		}

		private string _login;

		public string Login
		{
			get { return _login; }
			set {
				if(_login!= value)
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
			set {
				if(_password!= value)
				{
                    _password = value;
                    OnPropertyChanged();
                }
		
			}
		}




	}
}
