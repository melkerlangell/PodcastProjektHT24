using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeller
{
    public class Podcast
    {
        public string Titel {  get; set; }
        public string Url { get; set; }
        public string Beskrivning { get; set; }
        public DateTime PubliceringsDatum { get; set; }
        public string Kategori {  get; set; }


        public Podcast() { }
    }
}
