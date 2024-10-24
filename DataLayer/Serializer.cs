using Modeller;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Serialization;

namespace DataLayer
{
    public class Serializer<T>
    {
        private string filNamn = Path.Combine(Directory.GetCurrentDirectory(), "poddar.xml");
        private string filKategorier = Path.Combine(Directory.GetCurrentDirectory(), "allakategorier.xml");


        public Serializer()
        {
            //behövs för att kunna lägga till eget namn på podcasten
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
        }

        public void SparaPoddar(List<T> allaPoddar)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (FileStream fil = new FileStream(filNamn, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fil, allaPoddar); 
            }
        }

        public List<T> LasInPoddar()
        {
            if (!File.Exists(filNamn))
            {
                return new List<T>(); 
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (FileStream fil = new FileStream(filNamn, FileMode.Open, FileAccess.Read))
            {
                return (List<T>)serializer.Deserialize(fil); 
            }
        }

        public void SparaKategorier(List<T> allaKategorier)
        {
            XmlSerializer serializer = new XmlSerializer (typeof(List<T>));
            using(FileStream fil = new FileStream(filKategorier, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fil, allaKategorier);
            }
        }

        public List<T> LasInKategorier()
        {
            if (!File.Exists(filKategorier))
            {
                return new List<T>();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (FileStream fil = new FileStream(filKategorier, FileMode.Open, FileAccess.Read))
            {
                return (List<T>)serializer.Deserialize(fil);
            }
        }
    }

}
