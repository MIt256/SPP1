using System;
using System.Threading;
using TracerLib;
using TracerLib.Serialization;

namespace MyTracerApp
{
    class MainApp
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();
            //for ex
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();

            MainApp app = new MainApp();

            Thread thread1 = new Thread(new ParameterizedThreadStart(app.Method));

           thread1.Start(tracer);
           thread1.Join();
           // Console.WriteLine(tracer.GetTraceResult()); 

            //for json
            var jsonSerializer = new JsonSerializer();
            TraceResult traceResult = tracer.GetTraceResult();

            string json = jsonSerializer.Serialize(traceResult);
                        
            Console.WriteLine(json);
        }
        //method for tracing
        public void Method(object o)
        {
            Tracer tracer = (Tracer)o;
            tracer.StartTrace();
            Thread.Sleep(50);
            tracer.StopTrace();
        }
    }
}
