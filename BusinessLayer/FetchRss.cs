using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using Modeller;
using DataLayer;

namespace DataLayer
{
    public class FetchRss
    {
        private PodcastRepository poddRep;

        public FetchRss()
        {
            poddRep = new PodcastRepository();
        }

        public void FetchPodcast(string url)
        {
            Podcast enPodd = new Podcast();

            using (XmlReader reader = XmlReader.Create(url.Trim()))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                if (feed != null)
                {
                    enPodd.Titel = feed.Title.Text;

                    foreach (SyndicationItem item in feed.Items)
                    {
                        string title = item.Title.Text;
                        DateTime publishDate = item.PublishDate.DateTime;
                        string description = item.Summary?.Text ?? "Ingen beskrivning finns förs avsnittet";

                        enPodd.poddAvsnitt.Add(new Avsnitt(title, publishDate, description));
                    }

                    enPodd.AntalAvsnitt = enPodd.poddAvsnitt.Count;
                    poddRep.AddPodd(enPodd);  
                }
            }
        }

        public List<Podcast> GetPodcasts()
        {
            return poddRep.GetAllaPoddar();
        }
    }
}
