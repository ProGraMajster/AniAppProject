namespace AniAppProject.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        btnVersion.Text = "Wersja: " + AppInfo.Current.Version.ToString();
    }
}