using System.Collections.Concurrent;

namespace TracerLib
{
    //class for results of tracing
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTracer> _threadTracers;

        public TraceResult(ConcurrentDictionary<int, ThreadTracer> threadTracers)
        {
            _threadTracers = threadTracers;
        }

        internal ThreadTracer GetThreadTracer(int threadId)
        {
            return _threadTracers.GetOrAdd(threadId, new ThreadTracer(threadId));
        }

        public ConcurrentDictionary<int, ThreadTracer> GetThreadTracers()
        {
            return _threadTracers;
        }

    }
}
