using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AniAppProject.Popups;

public partial class SearchPopup : Popup
{
	public SearchPopup()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        GoToSearchPage();
    }

    void GoToSearchPage()
    {
        if(string.IsNullOrEmpty(searchBar.Text))
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "Pole wyszukiwania jest puste!";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            toast.Show(cancellationTokenSource.Token);
            return;
        }
        Close(searchBar.Text);
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        GoToSearchPage();
    }
}