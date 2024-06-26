using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject.Data
{
    public class EpisodesSeries
    {
        public string anime_id { get; set; }
        public int anime_episode_number { get; set; }
        public DateTime created_at { get; set; }
        public string isInverted { get; set; }
        public string bg { get; set; }
    }
}
