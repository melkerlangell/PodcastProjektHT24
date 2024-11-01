using System;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Modeller;
using System.Xml.Schema;
using System.Reflection;

namespace BusinessLayer
{
    public class PodcastController
    {
        private PoddRepository poddRep;
        private Validering validering;
        private List<System.Timers.Timer> timers;
        public event Action<string> PodcastUpdated;
        private int IntervallMs;

        public PodcastController()
        {
            poddRep = new PoddRepository();
            validering = new Validering();
            timers = new List<System.Timers.Timer>();
            IntervallMs = 60000;
        }

        public List<Podcast> getPoddar()
        {
            return poddRep.GetAll();
        }

        public void AndraPoddNamn(int i, string nyttNamn)
        {
            List<Podcast> poddar = poddRep.GetAll();
            if (validering.valideringIndex(i, poddar.Count))
            {
                poddar[i].EgetNamn = nyttNamn;
                poddRep.Update(i, poddar[i]);
            }
        }

        public void TaBortPodd(int index)
        {
            poddRep.Delete(index);
            timers[index].Stop();
        }


        public async Task FetchRssPoddar(string rssLank, string egetNamn, string kategori, int intervall)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (XmlReader minXMLlasare = XmlReader.Create(rssLank))
                    {
                        SyndicationFeed poddFlode = SyndicationFeed.Load(minXMLlasare);

                        Podcast enPodd = new Podcast
                        {
                            Titel = poddFlode.Title.Text,
                            EgetNamn = egetNamn,
                            Kategori = kategori,
                            UrlRss = rssLank,
                            uppdateringsIntervall = intervall,
                            poddAvsnitt = poddFlode.Items.Select(item => new Avsnitt
                            {
                                Title = item.Title.Text,
                                PublishDate = item.PublishDate.DateTime,
                                Description = item.Summary?.Text ?? "Ingen beskrivning finns tillgänglig"
                            }).ToList()
                        };

                        StartaTimerPaNyPodd(enPodd);

                        enPodd.AntalAvsnitt = enPodd.poddAvsnitt.Count;

                        poddRep.Insert(enPodd);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error vid hämtning av den nya podcasten: {ex.Message}");
            }
        }

        public async Task FetchBaraAvsnitt(Podcast p)
        {
            try
            {
                using (XmlReader minXMLlasare = XmlReader.Create(p.UrlRss))
                {
                    SyndicationFeed avsnittFlode = await Task.Run(() => SyndicationFeed.Load(minXMLlasare));

                   
                    p.poddAvsnitt.Clear();

                    foreach (var item in avsnittFlode.Items)
                    {
                        p.poddAvsnitt.Add(new Avsnitt
                        {
                            Title = item.Title.Text,
                            PublishDate = item.PublishDate.DateTime,
                            Description = item.Summary?.Text ?? "Ingen beskrivning finns tillgänglig"
                        });
                    }

                    int poddIndex = poddRep.GetAll().FindIndex(x => x.UrlRss.Equals(p.UrlRss));
                    poddRep.UppdateraAvsnittLista(p.poddAvsnitt, poddIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error vid hämtning av nya avsnitt: {ex.Message}");
            }
        }





        public void AndraPoddKategori(int podcastIndex, string nyKategori)
         {
            List<Podcast> poddar = getPoddar();
                if (validering.valideringIndex(podcastIndex, poddar.Count))
                {
                    Podcast valdPodd = poddar[podcastIndex];
                    valdPodd.Kategori = nyKategori;
                    poddRep.Update(podcastIndex, valdPodd);
                }
        }

        public void AndraPoddIntervall(int podcastIndex, string nyttIntervall)
        {
            List<Podcast> poddar = getPoddar();
            if (validering.valideringIndex(podcastIndex, poddar.Count()))
            {
                Podcast valdPodd = poddar[podcastIndex];
                valdPodd.uppdateringsIntervall = Int32.Parse(nyttIntervall);
                poddRep.Update(podcastIndex, valdPodd);

                timers[podcastIndex].Dispose();

                StartaTimerPaNyPodd(valdPodd, valdPodd.uppdateringsIntervall);
            }
        }


        public void UppdateraPodcastsKategori(string gammalKategori, string nyKategori)
        {
            List<Podcast> poddar = getPoddar();

            foreach (var podd in poddar)
            {
                
                if (podd.Kategori.Equals(gammalKategori))
                {
                    if (string.IsNullOrEmpty(nyKategori))
                    {
                        podd.Kategori = "-";  
                    }
                    else
                    {
                        podd.Kategori = nyKategori; 
                    }
                    poddRep.Update(poddar.IndexOf(podd), podd);
                }
            }
        }


        private async void StartaTimer(Podcast p, int? intervall = null)
        {
            if (p.uppdateringsIntervall <= 0)
            {
                return;
            }

            int interval = intervall ?? p.uppdateringsIntervall;

            System.Timers.Timer timer = new System.Timers.Timer()
            {
                Interval = interval * IntervallMs,
                AutoReset = true
            };

            timer.Elapsed += async (sender, args) =>
            {
                try
                {
                    await FetchBaraAvsnitt(p);
                    p.AntalAvsnitt = p.poddAvsnitt.Count;
                    PodcastUpdated?.Invoke($"'{p.Titel}' har uppdaterats {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    validering.visaFelmeddelande("Fel vid uppdatering av podcast " + p.Titel, ex);
                }
            };

            timers.Add(timer);
            timer.Start();
        }

        private async void StartaTimerPaNyPodd(Podcast p)
        {
            StartaTimer(p);
        }

        private async void StartaTimerPaNyPodd(Podcast p, int intervall)
        {
            StartaTimer(p, intervall);
        }

        public async void StartaTimersPaBefintligaPoddar()
        {
            foreach (Podcast p in getPoddar())
            {
                StartaTimer(p);
            }
        }

    }
}
