using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject
{
    public static class AdManager
    {
        public static class SeriesPageFullScreenAd
        {
            static int Max = 6;
            static int Current = 0;
            public static bool AddAndCheck()
            {
                SettingsManager.SeriesPageFullScreenAd.Get();
                Max = SettingsManager.SeriesPageFullScreenAd.Value;
                Current++;
                if(Current >= Max)
                {
                    Current = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
