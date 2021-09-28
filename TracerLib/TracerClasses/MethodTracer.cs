using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace TracerLib
{
    //tracer for methods
    public class MethodTracer
    {
        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string MethodName { get; set; }

        [JsonProperty("class")]
        [XmlAttribute("class")]
        public string ClassName { get; set; }

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public double Time { get; set; }

        [JsonProperty("methods")]
        [XmlElement("methods")]
        public List<MethodTracer> ChildMethodsList { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        private readonly byte[] _hash;

        [JsonIgnore]
        [XmlIgnore]
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public MethodTracer() { }
        public MethodTracer(string methodName, string className, byte[] hash)
        {
            MethodName = methodName;
            ClassName = className;
            _stopwatch.Start();
            _hash = hash;
        }
        public void SetMethods(List<MethodTracer> childMethods)
        {
            ChildMethodsList = childMethods;
        }
        public byte[] GetHash()
        {
            return _hash;
        }
        public void StopAndGetTime()
        {
            _stopwatch.Stop();
            Time = _stopwatch.ElapsedMilliseconds;
        }
        public long GetTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }
    }
}