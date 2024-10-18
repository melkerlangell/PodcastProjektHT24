using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer
{
    public class ValideringDAL
    {
        public bool ValidXml(string path)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(path))
                {
                    while (reader.Read()) { }
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            }
        }
    }

