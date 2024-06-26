using AniAppProject.Data;
using System.Diagnostics;

namespace AniAppProject.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    int pageNum = 1;
    int pageSize = 20;

    string _findCategory;
    bool flc = false;

    List<SeriesCategoryRespone> seriesCategoryRespones = new List<SeriesCategoryRespone>();

    public SearchPage(string category)
	{
		InitializeComponent();
		_findCategory = category;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (flc)
        {
            return;
        }
        if (!string.IsNullOrEmpty(_findCategory))
        {
            Thread thread = new Thread(async () =>
            {
                bool state = false;
                try
                {

                    var r = await AniAppProject.Services.DocchiApi.GetSeriesFromCategoryAsync(_findCategory);
                    if (r == null)
                    {
                        return;
                    }
                    seriesCategoryRespones = r;
                    var p = GetPage(seriesCategoryRespones, pageNum, pageSize);
                    var u = CreateUISerieView(p.ToList());
                    ChangeContent(u);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await sv.ScrollToAsync(0, 0, true);
                    });
                    state = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Debug.WriteLine(ex);
                }
                if (!state)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        flContent.Children.Add(new Label()
                        {
                            Text = "Wyst¹pi³ b³¹d! :("
                        });
                    });
                }
                IsLoading(false);
            });
            thread.Start();

        }
    }

    void IsLoading(bool state)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            aiLoading.IsVisible = state;
        });
    }

    List<UI.SerieView> CreateUISerieView(List<SeriesCategoryRespone> seriesCategoryRespones)
    {
        List<UI.SerieView> list = new List<UI.SerieView>();
        foreach (var item in seriesCategoryRespones)
        {
            PreSeries preSeries = new();
            preSeries.adult_content = item.adult_content;
            preSeries.cover = item.cover;
            preSeries.episodes = item.episodes;
            preSeries.genres = item.genres;
            preSeries.mal_id = item.mal_id;
            preSeries.season = item.season;
            preSeries.season_year = item.season_year;
            preSeries.series_type = item.series_type;
            preSeries.slug = item.slug;
            preSeries.title = item.title;
            preSeries.title_en = item.title_en;
            list.Add(new UI.SerieView(preSeries));
        }
        return list;
    }


    void ChangeContent(List<UI.SerieView> views)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            flContent.Children.Clear();
            //await sv.ScrollToAsync(0, 0, true);
            lblPageNum.Text = pageNum.ToString();
            foreach (var view in views)
            {
                flContent.Children.Add(view);
            }
        });
    }
    void ClearContent(List<UI.SerieView> views)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            flContent.Children.Clear();
            sv.ScrollToAsync(0, 0, true);
        });
    }

    async void GetNextPage()
    {

        pageNum++;
        if (pageNum > 1)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                btnPre.IsVisible = true;
            });
        }

        var l = seriesCategoryRespones;
        var p = GetPage(l, pageNum, pageSize);
        var u = CreateUISerieView(p.ToList());
        ChangeContent(u);
    }

    async void GetPreviuesPage()
    {
        pageNum--;
        if (pageNum <= 1)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                btnPre.IsVisible = false;
            });
        }
        var l = seriesCategoryRespones;
        var p = GetPage(l, pageNum, pageSize);
        var u = CreateUISerieView(p.ToList());
        ChangeContent(u);
    }

    IList<SeriesCategoryRespone> GetPage(IList<SeriesCategoryRespone> list, int page, int pageSize)
    {
        page = page - 1;
        var l = list.Skip(page * pageSize).Take(pageSize).ToList();
        return l;
    }

    private void btnPre_Clicked(object sender, EventArgs e)
    {
        Thread thread = new(() =>
        {
            IsLoading(true);
            GetPreviuesPage();
            IsLoading(false);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblPageNum.Text = pageNum.ToString();
            });
        });
        thread.Start();
    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {
        Thread thread = new(() =>
        {
            IsLoading(true);
            GetNextPage();
            IsLoading(false);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblPageNum.Text = pageNum.ToString();
            });
        });
        thread.Start();
    }



}