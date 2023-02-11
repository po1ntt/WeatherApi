using UraniumUI.Pages;
using WeatherApp.ViewsModels;
namespace WeatherApp.Views;

public partial class AuthorizePage : ContentPage
{
	public AuthorizePage()
	{
		InitializeComponent();
		if(Preferences.Default.Get("id_user", 0) != 0)
		{
		     Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}
      
	}
}