using System;

namespace TracerFolder
{
    public class Tracer : ITracer
    {
        public TraceResult GetTraceResult()
        {
            return new TraceResult();
        }

        public void StartTrace()
        {
            throw new NotImplementedException();
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }
    }
}
