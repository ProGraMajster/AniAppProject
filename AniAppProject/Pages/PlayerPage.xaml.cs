namespace AniAppProject.Pages;

public partial class PlayerPage : ContentPage
{
    string PlayerUrl;

    public PlayerPage(string playerUrl)
	{
		InitializeComponent();
        PlayerUrl = playerUrl;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        webView.Source = PlayerUrl;
    }

    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            webView.Source = "https://www.google.com";
            webView = null;
        });
        return base.OnBackButtonPressed();
    }
}