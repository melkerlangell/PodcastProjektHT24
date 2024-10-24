﻿using System;
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

namespace BusinessLayer
{
    public class PodcastController
    {
        private PoddRepository poddRep;
        private Validering validering;

        public PodcastController()
        {
            poddRep = new PoddRepository();
            validering = new Validering();
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
        }

       
        public void FetchRssPoddar(string rssLank, string egetNamn, string kategori)
        {
            XmlReader minXMLlasare = XmlReader.Create(rssLank);
            SyndicationFeed poddFlode = SyndicationFeed.Load(minXMLlasare);

            Podcast enPodd = new Podcast();
            enPodd.Titel = poddFlode.Title.Text;
            enPodd.EgetNamn = egetNamn;
            enPodd.Kategori = kategori;

            foreach (SyndicationItem item in poddFlode.Items)
            {
                Avsnitt ettAvsnitt = new Avsnitt();
                {
                    ettAvsnitt.Title = item.Title.Text;
                    ettAvsnitt.PublishDate = item.PublishDate.DateTime;
                    ettAvsnitt.Description = item.Summary?.Text ?? "Ingen beskrivning finns tillgänglig";
                };
                enPodd.poddAvsnitt.Add(ettAvsnitt);
            }
            enPodd.AntalAvsnitt = enPodd.poddAvsnitt.Count;

            poddRep.Insert(enPodd);
        }


        public void AndraPoddKategori(int podcastIndex, string nyKategori)
         {
            List<Podcast> poddar = poddRep.GetAll();
                if (validering.valideringIndex(podcastIndex, poddar.Count))
                {
                    Podcast valdPodd = poddar[podcastIndex];
                    valdPodd.Kategori = nyKategori;
                    poddRep.Update(podcastIndex, valdPodd);
                }
        }




        public void UppdateraPodcastsKategori(string gammalKategori, string nyKategori)
        {
            List<Podcast> poddar = poddRep.GetAll();

            foreach (var podd in poddar)
            {
                
                if (podd.Kategori == gammalKategori)
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
    }
}
