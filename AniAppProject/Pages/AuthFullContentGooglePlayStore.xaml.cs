namespace AniAppProject.Pages;

public partial class AuthFullContentGooglePlayStore : ContentPage
{
	public AuthFullContentGooglePlayStore()
	{
		InitializeComponent();
	}

    string code = "2137";

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if(e.NewTextValue == code)
            {
                entry.TextColor = Colors.Green;
            }
            else
            {
                entry.TextColor = Colors.Red;
            }
        }
        else
        {
            entry.TextColor= Colors.Red;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if(entry.Text == code)
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
                SettingsManager.AFCGPS.SetState(true);
            }
            else
            {
                await DisplayAlert("", "Nie poprawny kod!", "Ok");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}