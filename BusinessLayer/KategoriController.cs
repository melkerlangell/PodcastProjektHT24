using DataLayer.Repository;
using Modeller;
using System;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace BusinessLayer
{
    public class KategoriController
    {
        private KategoriRepository kategoriRep;

        public KategoriController()
        {
            kategoriRep = new KategoriRepository();
        }


        public List<Kategori> GetAllKategorier()
        {
            return kategoriRep.GetAll();
        }

        public Kategori GetKategori(string namn)
        {
            return kategoriRep.GetByID(namn);
        }

        public void TaBortKategori(int index)
        {
            kategoriRep.Delete(index); 
        }

        public void AndraKategori(int index, string nyttNamn)
        {
            Kategori enKategori = new Kategori(nyttNamn);
            kategoriRep.Update(index, enKategori);
        }

        public void LaggTillKategori(string namn){

            Kategori nyKategori = new Kategori(namn);
            kategoriRep.Insert(nyKategori);
            
        }
    }






}

