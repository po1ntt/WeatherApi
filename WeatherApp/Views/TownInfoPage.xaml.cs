using WeatherApp.Models;

namespace WeatherApp.Views;

public partial class TownInfoPage : ContentPage
{
	public TownInfoPage(Weather weather, Towns towns)
	{
		InitializeComponent();
		DateStart.MaximumDate = DateTime.Now.AddDays(13);
		DateEnd.MaximumDate = DateTime.Now.AddDays(13);
		DateStart.MinimumDate = DateTime.Now.AddMonths(-2);
        DateEnd.MinimumDate = DateTime.Now.AddMonths(-2);
        this.BindingContext = new ViewsModels.TownInfoVm(weather, towns);
	}
}