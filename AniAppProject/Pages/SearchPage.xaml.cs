using AniAppProject.Data;
using CommunityToolkit.Maui.Views;
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
    string _search;
    bool flc = false;

    List<SeriesCategoryRespone> seriesCategoryRespones = new List<SeriesCategoryRespone>();
    SearchRespone searchRespone;

    public SearchPage(string value=null, bool category=false, bool search=false)
	{
		InitializeComponent();
        if(value == null)
        {
            return;
        }
        if(category)
        {
            _findCategory = value;
        }
        else if(search)
        {
            _search = value;
        }

    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (flc)
        {
            return;
        }

#if ANDROID
                slAd.Children.Add(new Plugin.MauiMTAdmob.Controls.MTAdView()
                {
                    AdSize = Plugin.MauiMTAdmob.Extra.BannerSize.Banner,
                    AdsId = "ca-app-pub-3088807533847490/2152137882",
                    
                });
#endif

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

                    if (seriesCategoryRespones.Count < pageSize)
                    {
                        ChangeVisableNaviagtionPageContent(false);
                    }
                    else
                    {
                        ChangeVisableNaviagtionPageContent(true);
                    }

                    state = true;
                }
                catch (Exception ex)
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
                else
                {
                    CheckCountContent();
                }
                IsLoading(false);
            });
            thread.Start();
            

        }
        else if (!string.IsNullOrEmpty(_search))
        {
            Thread thread = new Thread(async () =>
            {
                bool state = false;
                try
                {

                    var r = await AniAppProject.Services.DocchiApi.GetSearchAsync(_search);
                    if (r == null)
                    {
                        return;
                    }
                    searchRespone = r;
                    var p = GetPage(searchRespone.series, pageNum, pageSize);
                    var u = CreateUISeriesViewFromSearchRespone(p.ToList(), false);
                    ChangeContent(u);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await sv.ScrollToAsync(0, 0, true);
                    });

                    if (searchRespone.series.Count < pageSize)
                    {
                        ChangeVisableNaviagtionPageContent(false);
                    }
                    else
                    {
                        ChangeVisableNaviagtionPageContent(true);
                    }

                    state = true;
                }
                catch (Exception ex)
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
                else
                {
                    CheckCountContent();
                }
                IsLoading(false);
            });
            thread.Start();
        }
    }

    void CheckCountContent()
    {
        if(flContent.Children.Count <= 0)
        {
            Label label = new();
            label.Text = "Brak winików :(";
            MainThread.BeginInvokeOnMainThread(() =>
            {
                flContent.Children.Add(label);
            });
        }
    }

    async void Reload()
    {
        IsLoading(true);
        ClearContent();

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
                    if (seriesCategoryRespones.Count < pageSize)
                    {
                        ChangeVisableNaviagtionPageContent(false);
                    }
                    else
                    {
                        ChangeVisableNaviagtionPageContent(true);
                    }
                    state = true;
                }
                catch (Exception ex)
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
                else
                {
                    CheckCountContent();
                }
                IsLoading(false);
            });
            thread.Start();

        }
        else if (!string.IsNullOrEmpty(_search))
        {
            Thread thread = new Thread(async () =>
            {
                bool state = false;
                try
                {

                    var r = await AniAppProject.Services.DocchiApi.GetSearchAsync(_search);
                    if (r == null)
                    {
                        return;
                    }
                    searchRespone = r;
                    var p = GetPage(searchRespone.series, pageNum, pageSize);
                    var u = CreateUISeriesViewFromSearchRespone(p.ToList(), false);
                    ChangeContent(u);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await sv.ScrollToAsync(0, 0, true);
                    });

                    if(searchRespone.series.Count < pageSize)
                    {
                        ChangeVisableNaviagtionPageContent(false);
                    }
                    else
                    {
                        ChangeVisableNaviagtionPageContent(true);
                    }

                    state = true;
                }
                catch (Exception ex)
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
                else
                {
                    CheckCountContent();
                }
                IsLoading(false);
            });
            thread.Start();
        }
    }

    void ChangeVisableNaviagtionPageContent(bool state)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            gridNaviagtion.IsVisible = state;
        });
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

    List<UI.SerieView> CreateUISeriesViewFromSearchRespone(List<SearchRespone.Series> series, bool h)
    {
        List<UI.SerieView> list = new List<UI.SerieView>();
        foreach (var item in series)
        {
            PreSeries preSeries = new();
            preSeries.adult_content = item.adult_content;
            preSeries.cover = item.cover;
            preSeries.episodes = item.episodes;
            preSeries.mal_id = item.mal_id;
            preSeries.season = item.season;
            preSeries.season_year = item.season_year;
            preSeries.series_type = item.series_type;
            preSeries.slug = item.slug;
            preSeries.title = item.title;
            preSeries.title_en = item.title_en;
            
            if (item.adult_content == "true")
            {
                if(h==true)
                {
                    list.Add(new UI.SerieView(preSeries));
                }
            }
            else
            {
                list.Add(new UI.SerieView(preSeries));
            }
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
    void ClearContent()
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

    IList<T> GetPage<T>(IList<T> list, int page, int pageSize)
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

    private async void ToolbarItemSearch_Clicked(object sender, EventArgs e)
    {
        try
        {
            Popups.SearchPopup popup = new Popups.SearchPopup();
            var r = await this.ShowPopupAsync(popup);
            if (r != null)
            {
                string txt = r.ToString();
                if (string.IsNullOrEmpty(txt))
                {
                    return;
                }
                _findCategory = null;
                _search = txt;
                Reload();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}