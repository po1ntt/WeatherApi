using WeatherApp.ViewsModels;

namespace WeatherApp.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
		BindingContext = new RegistrationVM();
	}
}