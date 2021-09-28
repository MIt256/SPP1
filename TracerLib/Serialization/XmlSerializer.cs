using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Formatting = System.Xml.Formatting;

namespace TracerLib.Serialization
{
    public class LXmlSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            var data = traceResult.GetThreadTracers().Values.ToArray();
            var xmlSerializer = new XmlSerializer(data.GetType());
            var stringWriter = new StringWriter();

            using (var writer = new XmlTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;
                xmlSerializer.Serialize(writer, data);
            }

            var result = stringWriter.ToString().Replace("ArrayOfThread", "root");

            return result;
        }
    }
}