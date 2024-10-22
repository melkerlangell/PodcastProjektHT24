using Modeller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class KategoriRepository : IRepository<Kategori>
    {
        Serializer<Kategori> Serializer;
        List<Kategori> ListKategori;

        public KategoriRepository()
        {
            Serializer = new Serializer<Kategori>();
            ListKategori = Serializer.LasInPoddar() ?? new List<Kategori>();
        }

        public List<Kategori> GetAll()
        {
            return Serializer.LasInPoddar();
        }

        public Kategori GetByID(string id)
        {
            Kategori katt = null;
            foreach (var item in Serializer.LasInPoddar())
            {
                if (item.Titel.Equals(id))
                {
                    katt = item;
                }
            }
            return katt;
        }
        public void Insert(Kategori theObject)
        {
            ListKategori = Serializer.LasInPoddar() ?? new List<Kategori>();
            ListKategori.Add(theObject);
            SaveChanges();
        }
        public void Update(int index, Kategori theNewObject)
        {
            if (index >= 0 && index < ListKategori.Count)
            {
                ListKategori[index] = theNewObject;
                SaveChanges();
            }
        }
        public void Delete(int index)
        {
            ListKategori.RemoveAt(index);
            SaveChanges();
        }
        public void SaveChanges()
        {
            Serializer.SparaPoddar(ListKategori);
        }
    }

}
