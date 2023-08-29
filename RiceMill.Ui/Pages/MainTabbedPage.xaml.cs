namespace RiceMill.Ui.Pages;

public partial class MainTabbedPage : TabbedPage
{
	public MainTabbedPage()
	{
		InitializeComponent();
		LoginPage._isFirstView = false;
    }

    protected override void OnAppearing()
    {
        CurrentPage = Children.Contains(Dashboard) ? Dashboard : RiceThreshing;
        base.OnAppearing();
    }
}