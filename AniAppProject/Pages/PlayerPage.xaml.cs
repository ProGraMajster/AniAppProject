namespace AniAppProject.Pages;

public partial class PlayerPage : ContentPage
{
    string PlayerUrl;

    public PlayerPage(string playerUrl)
	{
		InitializeComponent();
        PlayerUrl = playerUrl;
        webView.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36";
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