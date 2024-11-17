using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniAppProject.Data
{
    [Serializable]
    public class PromotedSeriesData
    {
        public string ImageSource { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public string CountEpisodes { get; set; }
        public string LengthEpisode { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}
