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
        List<Kategori> ListKategorier;

        public KategoriRepository()
        {
            Serializer = new Serializer<Kategori>();
            ListKategorier = Serializer.LasInKategorier();
        }
        public List<Kategori> GetAll()
        {
            return Serializer.LasInKategorier();
        }
       
        public void Insert(Kategori theObject)
        {
            ListKategorier.Add(theObject);
            SaveChanges();
        }
        public void Update(int index, Kategori theNewObject)
        {
            if (index >= 0 && index < ListKategorier.Count)
            {
                ListKategorier[index] = theNewObject;
                SaveChanges();
            }
        }
        public void Delete(int index)
        {
            ListKategorier.RemoveAt(index);
            SaveChanges();
        }
        public void SaveChanges()
        {
            Serializer.SparaKategorier(ListKategorier);
        }
    }
}
