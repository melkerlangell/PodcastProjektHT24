using Modeller;
using System.ServiceModel.Syndication;
using System.Xml.Serialization;

namespace DataLayer
{
    public class Serializer
    {
        ValideringDAL validering;

        public Serializer()
        {
            validering = new ValideringDAL();
        }
        public void SparaPoddar(List<Podcast> allaPoddar, string filNamn)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Podcast>));

            using (FileStream fil = new FileStream(filNamn+ ".xml", FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fil, allaPoddar);
            }
        }

        public List<Podcast> LasInPoddar()
        {
            string filNamn = "poddar.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Podcast>));

            using (FileStream fil = File.OpenRead(filNamn))
            {
                return (List<Podcast>)serializer.Deserialize(fil);
            }
        }
    }
}
