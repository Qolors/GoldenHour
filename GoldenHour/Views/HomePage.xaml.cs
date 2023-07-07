namespace GoldenHour.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
		var homeViewModel = BindingContext as HomeViewModel;

		if (homeViewModel != null)
		{
			await homeViewModel.LoadUpData();
		}

        base.OnNavigatedTo(args);
    }
}
