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
            
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();

            MainApp app = new MainApp();

            Thread thread1 = new Thread(new ParameterizedThreadStart(app.Method));

           thread1.Start(tracer);
           thread1.Join();
            
            //get results of tracing
            TraceResult traceResult = tracer.GetTraceResult();
            //for json
            var jsonSerializer = new JsonSerializer();            
            string json = jsonSerializer.Serialize(traceResult);                        
            Console.WriteLine(json);
            //for xml
            var xmlSerializer = new LXmlSerializer();
            string xml = xmlSerializer.Serialize(traceResult);
            Console.WriteLine(xml);
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
