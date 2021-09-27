using System.Collections.Generic;
//using System.Xml;
using Newtonsoft.Json;

namespace TracerLib.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            var arrays = new Dictionary<string, ICollection<ThreadTracer>>
            {
                {"threads", traceResult.GetThreadTracers().Values}
            };

            return JsonConvert.SerializeObject(arrays, Formatting.Indented);
        }
    }
}