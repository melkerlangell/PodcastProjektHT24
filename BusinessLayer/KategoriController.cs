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
using Modeller;
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


        public void DisplayKategorier()
        {
            List<Kategori> kategorier = kategoriRep.GetAll();
        }
        public void TaBortKategori(int index)
        {
            kategoriRep.Delete(index); 
        }

        public void AndraKategori(int index, Kategori nyKategori)
        {

            List<Kategori> kategorier = kategoriRep.GetAll();

            if (kategorier != null && index >= 0 && index < kategorier.Count)
            {
                kategorier[index] = nyKategori;
            }
        }

        public void LaggTillKategori(Kategori kategori){

            kategoriRep.Insert(kategori);
            
        }
    }






}
}
