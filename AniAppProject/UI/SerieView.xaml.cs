using AniAppProject.Data;

namespace AniAppProject.UI;

public partial class SerieView : ContentView
{
	PreSeries _preSeries;

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


    public SerieView(PreSeries preSeries)
	{
		InitializeComponent();
		try
		{
			if(preSeries == null )
			{ return; }
            _preSeries = preSeries;
            Title = preSeries.title;
            //labelName.Text = preSeries.title;
            btnImg.Source = preSeries.cover;
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
	}

    private async void btnImg_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new Pages.SeriesPage(_preSeries.slug));
    }
}