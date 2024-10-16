using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PoddAvsnitt
    {
        public string Url { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }

        public DateTime Datum { get; set; }

        public PoddAvsnitt() { }

        public override string ToString()
        {
            return Titel; // Return the title of the episode
        }
    }
}
