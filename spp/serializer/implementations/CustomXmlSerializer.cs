using spp.serializer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace spp.serializer.implementations
{
    public class CustomXmlSerializer<T> : Serializer<T>
    {
        private XmlSerializer serializer;
        public CustomXmlSerializer()
        {
            serializer = new XmlSerializer(typeof(T));
        }
        public string serialize(T obj)
        {
            using (var textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }
    }
}
