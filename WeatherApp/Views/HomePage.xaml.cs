using WeatherApp.Models;
using WeatherApp.Service;

namespace WeatherApp.Views;

public partial class HomePage : ContentPage
{
	private readonly IRestDataService _dataservice;
	public HomePage (IRestDataService dataService)
	{
		InitializeComponent();
		_dataservice = dataService;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		
	}

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{

	}
}