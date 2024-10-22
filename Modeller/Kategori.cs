using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeller
{
    [Serializable]
    public class Kategori
    {
        private string namn;

        public Kategori(string namn)
        {
            this.namn = namn;
        }

        public Kategori()
        {

        }


        public string Namn{get; set;}


    }
}
