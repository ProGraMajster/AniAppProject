using AniAppProject.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace AniAppProject.Pages;

public partial class BrowseSeriesPage : ContentPage
{
	public BrowseSeriesPage()
	{
		InitializeComponent();
	}
    int pageNum = 1;
    int pageSize = 20;

    async Task<List<PreSeries>> GetPreSeriesList(bool NoAdultContent = true)
    {
        var list = await AniAppProject.Services.DocchiApi.GetCechePreSeriesList();
        if (NoAdultContent)
        {
            List<PreSeries> listNoAdultContent = new List<PreSeries>();
            foreach (PreSeries preSeries in list)
            {
                if(preSeries.adult_content == "false")
                {
                    listNoAdultContent.Add(preSeries);
                }
            }
            return listNoAdultContent;
        }
        return list;
    }

    void IsLoading(bool state)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            aiLoading.IsVisible = state;
        });
    }

    bool flc = false;

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        btnPre.IsVisible = false;
        if(flc)
        {
            return;
        }
        Thread thread = new(async () =>
        {
            try
            {
                var l = await GetPreSeriesList();
                var p = GetPage(l,pageNum, pageSize);
                var u = CreateUISerieView(p.ToList());
                ChangeContent(u);
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await sv.ScrollToAsync(0, 0, true);
                });
            }
            catch(Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    flContent.Children.Add(new Label()
                    {
                        Text = "Wyst¹pi³ problem :( \n\n Szczegó³y: \n" +
                    ex.ToString()
                    });
                });
                Debug.WriteLine(ex);
                Console.WriteLine(ex);
            }
            IsLoading(false);
        });
        thread.Name = "LoadSeriesList";
        thread.Start();
        flc = true;
    }

    List<UI.SerieView> CreateUISerieView(List<PreSeries> preSeries)
    {
        List<UI.SerieView> list = new List<UI.SerieView>();
        foreach (var item in  preSeries)
        {
            list.Add(new UI.SerieView(item));
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

        var l = await GetPreSeriesList();
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
        var l = await GetPreSeriesList();
        var p = GetPage(l, pageNum, pageSize);
        var u = CreateUISerieView(p.ToList());
        ChangeContent(u);
    }

    IList<PreSeries> GetPage(IList<PreSeries> list, int page, int pageSize)
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