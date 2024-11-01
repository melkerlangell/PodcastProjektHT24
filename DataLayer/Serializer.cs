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
        private static readonly Object r = new Object();


        public Serializer()
        {
            //behövs för att kunna lägga till eget namn på podcasten
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
        }

        public void SparaPoddar(List<T> allaPoddar)
        {
            lock (r)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                try
                {
                    using (FileStream fil = new FileStream(filNamn, FileMode.Create, FileAccess.Write))
                    {
                        serializer.Serialize(fil, allaPoddar);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error vid sparande av poddar: {ex.Message}");
                }
            }
        }

        public List<T> LasInPoddar()
        {
            lock (r)
            {
                if (!File.Exists(filNamn))
                {
                    return new List<T>();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                try
                {
                    using (FileStream fil = new FileStream(filNamn, FileMode.Open, FileAccess.Read))
                    {
                        return (List<T>)serializer.Deserialize(fil);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error vid inläsning av poddar: {ex.Message}");
                    return new List<T>();
                }
            }
        }

        public void SparaKategorier(List<T> allaKategorier)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (FileStream fil = new FileStream(filKategorier, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fil, allaKategorier);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error vid sparande av kategorier: {ex.Message}");
            }
        }

        public List<T> LasInKategorier()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error vid inläsning av kategorier: {ex.Message}");
                return new List<T>();
            }
        }
    }

}
