﻿using DataLayer.Repository;
using Modeller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer
{
    public class KategoriController
    {
        private KategoriRepository katRep;
        private PodcastController poddKontroll;
        private Validering validering;

        public KategoriController()
        {
            katRep = new KategoriRepository();
            poddKontroll = new PodcastController(); 
            validering = new Validering();
        }

        
        public List<Kategori> getKategorier()
        {
            return katRep.GetAll();
        }

        public void LaggTillKat(string katNamn)
        {
            Kategori k = new Kategori();
            k.Namn = katNamn;
            katRep.Insert(k);
        }

        public void AndraKategoriNamn(int i, string nyttNamn)
        {
            List<Kategori> kategorier = katRep.GetAll();
            if (validering.valideringIndex(i,kategorier.Count))
            {
                kategorier[i].Namn = nyttNamn;

                katRep.Update(i, kategorier[i]);
            }
        }

        public void TaBortKategori(int index)
        {
            List<Kategori> kategorier = katRep.GetAll();
            if (validering.valideringIndex(index, kategorier.Count))
            {
                katRep.Delete(index);
            }
        }
    }
}
