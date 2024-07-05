using AniAppProject.Data;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AniAppProject.Services
{
    public static class DocchiApi
    {
        static List<PreSeries> CachePreSeriesList;
        static DateTime DateTimeCachePreSeriesList;

        public async static Task<List<PreSeries>> GetCechePreSeriesList()
        {
            if (CachePreSeriesList == null)
            {
                CachePreSeriesList = new List<PreSeries>();
            }
            if(CachePreSeriesList.Count <= 0)
            {
                CachePreSeriesList = await GetsAllSeries();
            }

            if(DateTime.UtcNow > DateTimeCachePreSeriesList.AddMinutes(20))
            {
                CachePreSeriesList = await GetsAllSeries();
            }

            return CachePreSeriesList;
        }

        //https://docchi.pl/api/search/search?string=dar

        public async static Task<SearchRespone> GetSearchAsync(string search)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://docchi.pl/api/search/search?string={search}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    var r = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchRespone>(html);

                    return r;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }



        public async static Task<List<PreSeries>> GetsAllSeries()
        {
            using (var httpClient = new HttpClient())
            {
                List<PreSeries> list = new List<PreSeries>();
                httpClient.BaseAddress = new Uri("https://api.docchi.pl/v1/series/list");

                //httpClient.DefaultRequestHeaders.Add("Referer", $"https://ebd.cda.pl/620x395/{id}");
                httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");

                try
                {
                    var response = await httpClient.GetAsync("https://api.docchi.pl/v1/series/list");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    list = System.Text.Json.JsonSerializer.Deserialize<List<PreSeries>>(html);
                    CachePreSeriesList = list;
                    DateTimeCachePreSeriesList = DateTime.UtcNow;
                    return list;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }
            }
            return null;
        }

        private static void AddHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
        }

        public async static Task<Series> GetSeriesAsync(string slug)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/series/find/{slug}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    var r = System.Text.Json.JsonSerializer.Deserialize<Series>(html);

                    return r;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }
        
        public async static Task<List<SeriesRelated>> GetSeriesrelatedAsync(string title, string mal_id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/series/find/{title}/{mal_id}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    var r = System.Text.Json.JsonSerializer.Deserialize<List<SeriesRelated>>(html);

                    return r;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        public async static Task<List<Data.EpisodesSeries>> GetsListOfHowMuchEpisodesSeriesContains(string slug)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/episodes/count/{slug}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    var r = System.Text.Json.JsonSerializer.Deserialize<List<EpisodesSeries>>(html);

                    return r;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        public async static Task<List<Data.PlayersForEpisode>> GetsListOfPlayersForEpisode(string slug, int number)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/episodes/find/{slug}/{number}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    var r = System.Text.Json.JsonSerializer.Deserialize<List<PlayersForEpisode>>(html);

                    return r;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        //https://api.docchi.pl/v1/series/category?name=Action&sort=undefine

        public async static Task<List<SeriesCategoryRespone>> GetSeriesFromCategoryAsync(string category)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/series/category?name={category}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    List<SeriesCategoryRespone> r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeriesCategoryRespone>>(html);

                    return r;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }


        public async static Task<List<Stats>> GetStatsAsync(string slug)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://api.docchi.pl/v1/series/animelist/stats/{slug}");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();

                    List<Stats> stat = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stats>>(html);

                    return stat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

        public async static Task<string> GetMainSite()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddHeaders(httpClient);
                    var response = await httpClient.GetAsync($"https://docchi.pl");
                    response.EnsureSuccessStatusCode();
                    var html = await response.Content.ReadAsStringAsync();
                    return html;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
            return null;
        }

    }
}
