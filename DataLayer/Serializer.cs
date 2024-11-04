using Modeller;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Serialization;

namespace DataLayer
{
    public class Serializer<T>
    {
        //skapar två dokument, ett för podcasts och ett för kategorier
        private string filNamn = Path.Combine(Directory.GetCurrentDirectory(), "poddar.xml");
        private string filKategorier = Path.Combine(Directory.GetCurrentDirectory(), "allakategorier.xml");
        private static readonly Object r = new Object();


        public Serializer()
        {
            //behövs för att kunna lägga till eget namn på podcasten
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
        }


        //logik för att jobba med podcast dokumentet
        public void SparaPoddar(List<T> allaPoddar)
        {
            //använder lock vilket gör att trådar som använder metoden ställer sig i kö för att vänta på varandra
            //behövs vid timern ifall flera poddar har nya avsnitt så blir det fel när det blir två skrivningar samtidigt i samma dokument
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


        //logik för att jobba med kategori dokumentet
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
