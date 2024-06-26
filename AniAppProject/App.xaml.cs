
namespace AniAppProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if ANDROID
Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.UserPersonalizedAds = true;
Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.ComplyWithFamilyPolicies = true;
Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.UseRestrictedDataProcessing = true;
Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.AdsId = "ca-app-pub-3088807533847490/2152137882";
Plugin.MauiMTAdmob.CrossMauiMTAdmob.Current.TestDevices = new List<string>() { };
    if (SettingsManager.AFCGPS.GetState())
            {
                MainPage = new Pages.AuthFullContentGooglePlayStore();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
#else
            MainPage = new NavigationPage(new MainPage());
#endif
        }
    }
}
