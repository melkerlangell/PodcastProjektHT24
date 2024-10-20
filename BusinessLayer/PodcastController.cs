﻿using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Modeller;

namespace BusinessLayer
{
    public class PodcastController
    {
        private PoddRepository poddRep;

        public PodcastController()
        {
            poddRep = new PoddRepository();
        }

        public List<Podcast> getPoddar()
        {
            return poddRep.GetAll();
        }


        public void TaBortPodd(int index)
        {
            poddRep.Delete(index);
        }
        
        public void FetchRssPoddar(string rssLank, string egetNamn)
        {
            XmlReader minXMLlasare = XmlReader.Create(rssLank);
            SyndicationFeed poddFlode = SyndicationFeed.Load(minXMLlasare);

            Podcast enPodd = new Podcast();
            enPodd.Titel = poddFlode.Title.Text;
            enPodd.EgetNamn = egetNamn;

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
    }
}
