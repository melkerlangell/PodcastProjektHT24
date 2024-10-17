using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeller;

namespace DataLayer
{
    public class PodcastRepository
    {
        private List<Podcast> allaPoddar = new List<Podcast>();

        public void AddPodd(Podcast podcast)
        {
            allaPoddar.Add(podcast);
        }

        public List<Podcast> GetAllaPoddar()
        {
            return allaPoddar;
        }
    }
}
