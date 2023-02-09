using WeatherApp.Models;

namespace WeatherApp.Views;

public partial class TownInfoPage : ContentPage
{
	public TownInfoPage(Weather weather, Towns towns)
	{
		InitializeComponent();
		this.BindingContext = new ViewsModels.TownInfoVm(weather, towns);
	}
}