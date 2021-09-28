using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLib
{
    //tracer for threads
    [XmlType("thread")]
    public class ThreadTracer
    {
        [JsonProperty("id")]
        [XmlAttribute("id")]
        public int ThreadId { get; set; }

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public long ThreadTime { get; set; }

        [JsonProperty("methods")]
        [XmlElement("methods")]
        public List<MethodTracer> MethodTracerList { get; set; }

        public ThreadTracer() { }
        public ThreadTracer(int threadId)
        {
            ThreadId = threadId;
            MethodTracerList = new List<MethodTracer>();
        }
        public void PushMethod(string methodName, string className, byte[] hash)
        {
            MethodTracerList.Add(new MethodTracer(methodName, className, hash));
        }
        public void PopMethod(byte[] hash)
        {
            int index = MethodTracerList.FindLastIndex(item => HashEquals(item.GetHash(), hash));
            if (index != MethodTracerList.Count - 1)
            {
                SetChildMethods(index);
            }
            ThreadTime += MethodTracerList[index].GetTime();
            MethodTracerList[index].StopAndGetTime();
        }
        private void SetChildMethods(int index)
        {
            int size = MethodTracerList.Count - index - 1;
            List<MethodTracer> childMethods = MethodTracerList.GetRange(index + 1, size);
            for (var i = 0; i < size; i++)
                MethodTracerList.RemoveAt(MethodTracerList.Count - 1);
            MethodTracerList[index].SetMethods(childMethods);
            MethodTracerList[index].StopAndGetTime();
        }
        private bool HashEquals(byte[] hash, byte[] newHash)
        {
            bool bEqual = false;
            if (newHash.Length == hash.Length)
            {
                int i = 0;
                while ((i < newHash.Length) && (newHash[i] == hash[i]))
                {
                    i += 1;
                }
                if (i == newHash.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }
    }
}