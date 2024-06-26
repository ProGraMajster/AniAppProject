using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject.Data
{
    public class SeriesRelated
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
