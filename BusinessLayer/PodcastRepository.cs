using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Modeller;
using DataLayer;


namespace BusinessLayer
{
    public class PodcastRepository
    {
        private List<Podcast> allaPoddar;
        Serializer serializer = new Serializer();

        public PodcastRepository()
        {
            allaPoddar = serializer.LasInPoddar();
        }

        public void AddPodd(Podcast podcast)
        {
            allaPoddar.Add(podcast);
            serializer.SparaPoddar(allaPoddar, "poddar");
        }

        public List<Podcast> GetAllaPoddar()
        {
            return allaPoddar;
        }
    }
}
