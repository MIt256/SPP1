using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLib
{
    //tracer for methods
    public class MethodTracer
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public double Time { get; set; }

        public List<MethodTracer> ChildMethodsList { get; set; }

        private readonly byte[] _hash;

        private readonly Stopwatch _stopwatch = new Stopwatch();

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
