namespace AniAppProject.UI;

public partial class GroupView : ContentView
{
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

    public GroupView()
	{
		InitializeComponent();
	}

    public void AddView(View view)
    {
        hsl.Children.Add(view);
    }

    public void RemoveView(View view)
    {
        hsl.Children.Remove(view);
    }
}