using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using Modeller;


namespace DataLayer
{
    public class FetchRss
    {
        private Repository.PoddRepository poddRep;

        public FetchRss()
        {
            poddRep = new Repository.PoddRepository();
        }

        public void FetchPodcast(string url)
        {
            Podcast enPodd = new Podcast();
            try
            {
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
                            string description = item.Summary?.Text ?? "No description available";


                            enPodd.poddAvsnitt.Add(new Avsnitt(title, publishDate, description));
                        }

                        enPodd.AntalAvsnitt = enPodd.poddAvsnitt.Count;
                        poddRep.AddPodd(enPodd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching podcast: {ex.Message}");
            }
        }

        public List<Podcast> GetPodcasts()
        {
            return poddRep.GetAllaPoddar();
        }
    }
}
