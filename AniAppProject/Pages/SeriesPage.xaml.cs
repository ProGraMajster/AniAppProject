using AniAppProject.Data;
using AniAppProject.UI;
using Plugin.MauiMTAdmob;
using System.Diagnostics;
namespace AniAppProject.Pages;

public partial class SeriesPage : ContentPage
{
	Series Serie { get; set; }
	string Slug { get; set; }

	public SeriesPage(string slug)
	{
		InitializeComponent();
        Slug = slug;
    }

	bool flc = false;
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		try
		{
			if(flc)
			{
				return;
			}
#if ANDROID
			Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-3088807533847490/5601347516");
#endif
            Thread thread = new Thread(async () =>
			{
				Serie = await AniAppProject.Services.DocchiApi.GetSeriesAsync(Slug);
				if(Serie == null)
				{
					await DisplayAlert("Wyst¹pi³ b³¹d", "Nie uda³o siê za³adowaæ treœci", "Ok");
					await Navigation.PopAsync();
					return;
				}

				Thread threadEpisodes = new(async () =>
				{
					try
					{
						var eps = await AniAppProject.Services.DocchiApi.GetsListOfHowMuchEpisodesSeriesContains(Slug);
						if(eps == null)
						{
							return;
						}
						MainThread.BeginInvokeOnMainThread(() => {
							foreach (var episode in eps)
							{
								slEps.Children.Add(new EpisodesSeriesView(episode, Slug));
							}
						});
					}
					catch(Exception ex)
					{
                        Console.WriteLine(ex.ToString());
                    }
				});
                threadEpisodes.Name = "threadEpisodes";
                threadEpisodes.Start();

				Thread threadStats = new(async () =>
				{
					try
					{
						var stats = await AniAppProject.Services.DocchiApi.GetStatsAsync(Slug);
						if(stats == null)
						{
							return;
						}
						MainThread.BeginInvokeOnMainThread(() =>
						{
							slStats.Children.Add(new StatSeriesView(stats));
						});
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.ToString());
						Debug.WriteLine(ex.ToString());
					}
				});
				threadStats.Name = "threadStats";
                threadStats.Start();

                //ca-app-pub-3088807533847490/4658682034


                MainThread.BeginInvokeOnMainThread(() =>
				{
#if ANDROID
					if(AdManager.SeriesPageFullScreenAd.AddAndCheck())
					{
                        CrossMauiMTAdmob.Current.ShowInterstitial();
                    }
#endif
					try
					{
						lblTitle.Text = Serie.title;
						Title = lblTitle.Text;
						lblEnTitle.Text = Serie.title_en;
						lblEpisodes.Text = Serie.episodes.ToString();
                        lblbroadcast_day.Text = Serie.broadcast_day;
						lblSeason.Text = Serie.season + " "+ Serie.season_year;
						lblDes.Text = Serie.description;
                        lblType.Text = Serie.series_type;

                        foreach (var g in Serie.genres)
						{
							Button button = new Button();
							button.Text = g;
							button.CommandParameter = g;
                            button.Clicked += ButtonGenres_Clicked;
                            slG.Children.Add(button);
                        }

                        imgAnimePicture.Source = Serie.cover;
						aiLoading.IsVisible = false;

#if ANDROID
                        slAd.Children.Add(new Plugin.MauiMTAdmob.Controls.MTAdView()
                        {
                            AdSize = Plugin.MauiMTAdmob.Extra.BannerSize.Banner,
                            AdsId = "ca-app-pub-3088807533847490/8638745837",

                        });
#endif

                    }
                    catch (Exception ex2)
					{
                        Console.WriteLine(ex2.ToString());
                    }
                });

			});
			thread.Start();
			flc = true;
		}
		catch(Exception ex)
		{
            Console.WriteLine(ex.ToString());
        }
    }

    private async void ButtonGenres_Clicked(object sender, EventArgs e)
    {
		try
		{
			Button button = (Button)sender;
			string p = button.CommandParameter.ToString();
			if(string.IsNullOrEmpty(p))
			{
				return;
			}
			await Navigation.PushAsync(new Pages.SearchPage(p));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void btnMal_Clicked(object sender, EventArgs e)
    {
		try
		{
			Launcher.Default.OpenAsync("https://myanimelist.net/anime/" + Serie.mal_id);
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
    }
}