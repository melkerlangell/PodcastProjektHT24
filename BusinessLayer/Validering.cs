using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer
{
    public class Validering
    {
        public Validering() { }

        public string visaFelmeddelande(string felmeddelande, Exception ex)
        {
            return $"Error:" + felmeddelande + ex.Message;
        }

        public bool valideringNamn(string podcastNamn)
        {
            return !string.IsNullOrWhiteSpace(podcastNamn);
        }

        public bool valideringIndex(int index, int count)
        {
            return index >= 0 && index < count;
        }

        public bool valideringXml(string path)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(path))
                {
                    while (reader.Read()) { }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


