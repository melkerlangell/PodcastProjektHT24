using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeller
{
    public class Kategori
    {
        public List<String> KategoriList = new List<String>();
        public string Titel { get; set; }




        public Kategori()
        {
            KategoriList.Add("Humor");
            KategoriList.Add("Sport");
            KategoriList.Add("Kultur");
        }

     
        public void TaBortKatergori(String kategori)
        {
            foreach (String item in KategoriList)
            {
                if (item == kategori)
                {
                    int Index = KategoriList.IndexOf(item);
                    KategoriList.RemoveAt(Index);

                }
            }

        }

     

    

 


    }

   

}
