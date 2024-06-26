using AniAppProject.Data;
using System.Diagnostics;

namespace AniAppProject.UI;

public partial class PlayersForEpisodeView : ContentView
{
	PlayersForEpisode PlayersForEpisode { get; set; }
	public PlayersForEpisodeView(PlayersForEpisode players)
	{
		InitializeComponent();
        PlayersForEpisode = players;
        btnTran.Text = PlayersForEpisode.translator_title;
        lblHosting.Text = PlayersForEpisode.player_hosting;
        if (PlayersForEpisode.translator == "true")
        {
            lblT.Text = "Tak";
        }
        else
        {
            lblT.Text = "Nie";
        }

    }

    private async void btnWatch_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new Pages.PlayerPage(PlayersForEpisode.player));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex.ToString());
        }
    }
}