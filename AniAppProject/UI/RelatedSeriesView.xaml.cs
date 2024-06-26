namespace AniAppProject.UI;

public partial class RelatedSeriesView : ContentView
{
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource),
            typeof(string), typeof(RelatedSeriesView), string.Empty);

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(RelatedSeriesView),
            string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty RelatedTypeProperty =
        BindableProperty.Create(nameof(RelatedType),
            typeof(string),
            typeof(RelatedSeriesView),
            string.Empty);

    public string RelatedType
    {
        get => (string)GetValue(RelatedTypeProperty);
        set => SetValue(RelatedTypeProperty, value);
    }

    public static readonly BindableProperty TitleEnProperty =
        BindableProperty.Create(nameof(TitleEn),
            typeof(string),
            typeof(RelatedSeriesView),
            string.Empty);

    public string TitleEn
    {
        get => (string)GetValue(TitleEnProperty);
        set => SetValue(TitleEnProperty, value);
    }

    public RelatedSeriesView()
	{
		InitializeComponent();
	}
}