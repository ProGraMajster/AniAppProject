using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAppProject.Data
{
    public class PlayersForEpisode
    {
        public int id { get; set; }
        public string anime_id { get; set; }
        public int anime_episode_number { get; set; }
        public string player { get; set; }
        public string player_hosting { get; set; }
        public DateTime created_at { get; set; }
        public string translator { get; set; }
        public string translator_title { get; set; }
        public string translator_url { get; set; }
        public string isInverted { get; set; }
        public object bg { get; set; }
    }
}
