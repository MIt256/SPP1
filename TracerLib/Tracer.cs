using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics;
using System;
using System.Text;
using System.Security.Cryptography;

namespace TracerLib
{
    //main class for tracer, redefining the methods
    public class Tracer : ITracer
    {
        private readonly TraceResult _traceResult;
        private readonly MD5CryptoServiceProvider _md5;

        public Tracer()
        {
            _traceResult = new TraceResult(new ConcurrentDictionary<int, ThreadTracer>());
            _md5 = new MD5CryptoServiceProvider();
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            ThreadTracer threadTracer = _traceResult.GetThreadTracer(Thread.CurrentThread.ManagedThreadId);

            var stackTrace = new StackTrace();

            string[] path = stackTrace.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            path[0] = "";

            byte[] bytesPath = ASCIIEncoding.ASCII.GetBytes(string.Join("", path));
            byte[] hash = _md5.ComputeHash(bytesPath);

            string methodName = stackTrace.GetFrames()[1].GetMethod().Name;
            string className = stackTrace.GetFrames()[1].GetMethod().ReflectedType.Name;

            threadTracer.PushMethod(methodName, className, hash);
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = _traceResult.GetThreadTracer(Thread.CurrentThread.ManagedThreadId);

            string[] path = new StackTrace().ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            path[0] = "";

            byte[] bytesPath = ASCIIEncoding.ASCII.GetBytes(string.Join("", path));
            byte[] hash = _md5.ComputeHash(bytesPath);

            threadTracer.PopMethod(hash);
        }
    }
}
