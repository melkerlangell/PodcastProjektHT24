using DataLayer.Repository;
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

        public KategoriController()
        {
            katRep = new KategoriRepository();
            poddKontroll = new PodcastController(); 
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
            if (i >= 0 && i < kategorier.Count)
            {
                string gammaltNamn = kategorier[i].Namn;
                kategorier[i].Namn = nyttNamn;
                katRep.Update(i, kategorier[i]);

                poddKontroll.UppdateraPodcastsKategori(gammaltNamn, nyttNamn);
            }
        }

        public void TaBortKategori(int index)
        {
            List<Kategori> kategorier = katRep.GetAll();
            if (index >= 0 && index < kategorier.Count)
            {
                string gammalKategori = kategorier[index].Namn;
                katRep.Delete(index);

                poddKontroll.UppdateraPodcastsKategori(gammalKategori, "Ingen kategori");
            }
        }


        //public void AndraPoddKategori(int podcastIndex, string nyKategori)
        //{
        //    // Anropa funktionen från PodcastController för att uppdatera kategorin
        //    poddKontroll.AndraPoddKategori(podcastIndex, nyKategori);
        //}



    }






}
