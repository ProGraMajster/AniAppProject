using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject.Data
{
    public class SearchRespone
    {
        public List<AniAppProject.Data.SearchRespone.Series> series { get; set; }
        public List<AniAppProject.Data.SearchRespone.Profile> profiles { get; set; }
        public class Profile
        {
            public string id { get; set; }
            public int to_xid { get; set; }
            public string display { get; set; }
            public string avatar { get; set; }
            public DateTime created_at { get; set; }
        }

        public class Series
        {
            public int mal_id { get; set; }
            public object ani_id { get; set; }
            public string title { get; set; }
            public string title_en { get; set; }
            public string slug { get; set; }
            public string cover { get; set; }
            public string adult_content { get; set; }
            public string series_type { get; set; }
            public int episodes { get; set; }
            public string season { get; set; }
            public int season_year { get; set; }
        }


    }

}
