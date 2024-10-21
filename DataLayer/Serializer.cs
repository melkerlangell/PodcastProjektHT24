using Modeller;
using System.ServiceModel.Syndication;
using System.Xml.Serialization;

namespace DataLayer
{
    public class Serializer<T>
    {
        ValideringDAL validering;
        private string filNamn;
        public string FilNamn
        {
            set
            {
                filNamn = value;
            }
        }

        public Serializer(string fNamn)
        {
            validering = new ValideringDAL();
            FilNamn = fNamn + ".xml";
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
            List<T> poddListan;
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            

                using (FileStream fil = new FileStream(filNamn, FileMode.Open, FileAccess.Read))
            {
                    return (List<T>)serializer.Deserialize(fil);
            }
            return poddListan;
        }
    }
}
