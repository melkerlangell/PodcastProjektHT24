using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeller;

namespace DataLayer.Repository
{
    public class PoddRepository : IRepository<Podcast>
    {
        Serializer<Podcast> Serializer;
        List<Podcast> ListPoddar;

        public PoddRepository()
        {
            Serializer = new Serializer<Podcast>();
            ListPoddar = Serializer.LasInPoddar();
        }

        public List<Podcast> GetAll()
        {
            return Serializer.LasInPoddar();
        }
       
        public void Insert(Podcast theObject)
        {
            ListPoddar.Add(theObject);
            SaveChanges();
        }

        public void Update(int index, Podcast theNewObject)
        {
            if (index >= 0 && index < ListPoddar.Count)
            {
                ListPoddar[index] = theNewObject;
                SaveChanges();
            }
        }

        public void Delete(int index)
        {
            ListPoddar.RemoveAt(index);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Serializer.SparaPoddar(ListPoddar);
        }
    }
}
