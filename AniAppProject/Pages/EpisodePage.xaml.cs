
using AniAppProject.UI;
using System.Diagnostics;
namespace AniAppProject.Pages;

public partial class EpisodePage : ContentPage
{
	string Slug;
	int Number;
	public EpisodePage(string slug, int number)
	{
		InitializeComponent();
		Slug = slug;
		Number = number;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		try
		{
            MainThread.BeginInvokeOnMainThread(() =>
            {
				this.Title = "Odcinek " + Number;
                slC.Children.Clear();
            });
            Thread thread = new(async () =>
			{
				try
				{
					var r = await AniAppProject.Services.DocchiApi.GetsListOfPlayersForEpisode(Slug, Number);
					if(r==null)
					{
						return;
					}
					MainThread.BeginInvokeOnMainThread(() =>
					{
						foreach (var item in r)
						{
							slC.Children.Add(new UI.PlayersForEpisodeView(item));
						}
					});
				}
				catch(Exception ex2)
				{
					Console.WriteLine(ex2.ToString());
				}
			});
			thread.Start();
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
    }

    private async void btnBackToSeriesPage_Clicked(object sender, EventArgs e)
    {
		try
		{
			EpisodePage episodePage = this;
			await Navigation.PushAsync(new Pages.SeriesPage(Slug));
			Navigation.RemovePage(episodePage);
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
			Debug.WriteLine(ex.ToString());
		}
    }
}