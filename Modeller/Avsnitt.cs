using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeller
{
    [Serializable]
    public class Avsnitt
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public Avsnitt(string title, DateTime publishDate, string description)
        {
            Title = title;
            PublishDate = publishDate;
            Description = description;
        }

        public Avsnitt() { }

      
        public override string ToString()
        {
            return Title; 
        }
    }
}

