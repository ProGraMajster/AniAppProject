using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject
{
    public static class SettingsManager
    {
        public static string pathSetting = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            "//AppSettings//";
        public static class AFCGPS
        {
            public static bool GetState()
            {
                try
                {
                    if (!File.Exists(pathSetting + "AFCGPS.txt"))
                    {
                        return false;
                    }
                    string r = File.ReadAllText(pathSetting + "AFCGPS.txt");
                    if(r == "true")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
                return false;
            }
            public static void SetState(bool value)
            {
                try
                {
                    File.WriteAllText(pathSetting + "AFCGPS.txt",value.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
