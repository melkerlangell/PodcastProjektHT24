using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using BLL;


namespace DAL
{
    public class HamtaPodcast
    {
        public HamtaPodcast() { }


        public async Task<List<PoddAvsnitt>> GetRss(string rssFeedUrl)
        {
            List<PoddAvsnitt> episodes = new List<PoddAvsnitt>();

            try
            {
                episodes = await Task.Run(() =>
                {
                    using (XmlReader reader = XmlReader.Create(rssFeedUrl))
                    {
                        SyndicationFeed feed = SyndicationFeed.Load(reader);
                        foreach (SyndicationItem item in feed.Items)
                        {
                            PoddAvsnitt episode = new PoddAvsnitt
                            {
                                Titel = item.Title?.Text ?? "No title",
                                Beskrivning = item.Summary?.Text ?? "No description",
                                Datum = item.PublishDate.DateTime,
                                Url = item.Links.Count > 0 ? item.Links[0].Uri.ToString() : string.Empty
                            };

                            episodes.Add(episode);
                        }
                    }
                    return episodes;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching or parsing RSS feed: {ex.Message}");
            }

            return episodes;
        }
    }
}

