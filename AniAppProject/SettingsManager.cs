using System.Diagnostics;

namespace AniAppProject
{
    public static class SettingsManager
    {
        public static string pathSetting = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
            "//AppSettings//";

        public static void CheckSettingsDir()
        {
            if(!Directory.Exists(pathSetting))
            {
                Directory.CreateDirectory(pathSetting);
            }
        }

        public static class SeriesPageFullScreenAd
        {
            public static int Value = 6;

            public static void Set(int value)
            {
                try
                {
                    Helper.SetIntState(pathSetting+ "SeriesPageFullScreenAd.txt", value);
                    Value = value;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            } 

            public static void Get()
            {
                try
                {
                    Value= Helper.GetIntState(pathSetting + "SeriesPageFullScreenAd.txt");
                    if(Value == -1)
                    {
                        Value = 5;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            }


        }

        static class Helper
        {
            public static int GetIntState(string path)
            {
                try
                {
                    if (!File.Exists(path))
                    {
                        return -1;
                    }
                    string txt = File.ReadAllText(path);
                    return int.Parse(txt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
                return -1;
            }

            public static void SetIntState(string path, int value)
            {
                try
                {
                    File.WriteAllText(path, value.ToString());
                }
                catch(Exception ex)
                { 
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            }


            public static bool GetBoolState(string path)
            {
                try
                {
                    if (!File.Exists(path))
                    {
                        return false;
                    }
                    string r = File.ReadAllText(path);
                    if (r == "true")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
                return false;
            }

            public static void SetBoolState(string path, bool value)
            {
                try
                {
                    File.WriteAllText(path, value.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        public static class AFCGPS
        {
            public static bool GetState()
            {
                try
                {
                    return true;
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
