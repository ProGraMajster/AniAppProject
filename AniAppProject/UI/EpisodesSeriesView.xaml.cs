using AniAppProject.Data;

namespace AniAppProject.UI;

public partial class EpisodesSeriesView : ContentView
{
	EpisodesSeries Episodes;
    string Slug;
	public EpisodesSeriesView(EpisodesSeries episode, string slug)
	{
		InitializeComponent();
		Episodes = episode;
        Slug= slug;
    }

	bool flc = false;

    private void ContentView_Loaded(object sender, EventArgs e)
    {
		try
		{
            if (flc) { return; }
            if (Episodes == null) { return; }

            lblEp.Text = "Odcinek " + Episodes.anime_episode_number;
            lblDate.Text = Episodes.created_at.Date.ToString("dd.mm.yy");
			img.Source = Episodes.bg;
            flc=true;
        }
		catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
    }

    private async void img_Clicked(object sender, EventArgs e)
    {
        try
        {
            if(Slug == null) { return; }
            await Navigation.PushAsync(new Pages.EpisodePage(Slug, Episodes.anime_episode_number));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Slug == null) { return; }
            await Navigation.PushAsync(new Pages.EpisodePage(Slug, Episodes.anime_episode_number));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}