using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeller
{
    [Serializable]
    public class Podcast
    {
        public string Titel {  get; set; }
        public int AntalAvsnitt { get; set; }
        
        public List<Avsnitt> poddAvsnitt { get; set; }
        public string Kategori {  get; set; }


        public Podcast() 
        {
            poddAvsnitt = new List<Avsnitt>();
        }
    }
}
