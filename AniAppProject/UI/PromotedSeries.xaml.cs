using System.Diagnostics;

namespace AniAppProject.UI;

public partial class PromotedSeries : ContentView
{
	public static readonly BindableProperty ImageSourceProperty =
		BindableProperty.Create(nameof(ImageSource),
			typeof(string), typeof(PromotedSeries), string.Empty);

    public string ImageSource
    {
		get => (string)GetValue(ImageSourceProperty);
		set => SetValue(ImageSourceProperty, value);
	}

	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), 
			typeof(string),
			typeof(PromotedSeries),
			string.Empty);

	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public static readonly BindableProperty TitleTypeProperty =
		BindableProperty.Create(nameof(TitleType),
			typeof(string),
			typeof(PromotedSeries),
			string.Empty);

	public string TitleType
	{
		get => (string)GetValue(TitleTypeProperty);
		set => SetValue(TitleTypeProperty, value);
	}

	public static readonly BindableProperty CountEpisodesProperty =
		BindableProperty.Create(nameof(CountEpisodes),
			typeof(string),
			typeof(PromotedSeries),
			string.Empty);

	public string CountEpisodes
    {
		get => (string)GetValue(CountEpisodesProperty);
		set => SetValue(CountEpisodesProperty, value);
	}

	public static readonly BindableProperty LengthEpisodeProperty =
		BindableProperty.Create(nameof(LengthEpisode),
			typeof(string),
			typeof(PromotedSeries),
			string.Empty);

	public string LengthEpisode
    {
		get => (string)GetValue(LengthEpisodeProperty);
		set => SetValue(LengthEpisodeProperty, value);
	}

	public static readonly BindableProperty DescriptionProperty =
		BindableProperty.Create(nameof(Description),
			typeof(string),
			typeof(PromotedSeries),
			string.Empty);

	public string Description
    {
		get => (string)GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}

	public string Slug { get; set; }

    public PromotedSeries()
	{
		InitializeComponent();
	}

    private async void btnMore_Clicked(object sender, EventArgs e)
    {
		try
		{
			await Navigation.PushAsync(new Pages.SeriesPage(Slug)); 
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex.ToString());
			Console.WriteLine(ex.ToString());
		}
    }

    private async void btnWatch_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new Pages.EpisodePage(Slug,1));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            Console.WriteLine(ex.ToString());
        }
    }

    private async void Image_Loaded(object sender, EventArgs e)
    {
		try
		{
            var res = await image.Source.GetPlatformImageAsync(Handler.MauiContext);
            if (res == null)
            {
				image.Source = ImageSource;
            }
        }
		catch(Exception ex)
		{
			Debug.WriteLine("Image_Loaded: \n"+ ex.ToString());
		}
    }
}