using System;
using System.Threading;
using TracerLib;
using TracerLib.Serialization;
using App.Output;
using System.IO;

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
            //Console.WriteLine(json);
            //for xml
            var xmlSerializer = new LXmlSerializer();
            string xml = xmlSerializer.Serialize(traceResult);
            //Console.WriteLine(xml);

            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var dataPath = Path.Combine(projectDirectory, "Files");

            var filePrinter = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.xml")));
            var consolePrinter = new ConsolePrinter();
            //for xml
            filePrinter.PrintResult(xml);
            consolePrinter.PrintResult(xml);

            filePrinter = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.json")));
            //for json
            filePrinter.PrintResult(json);
            consolePrinter.PrintResult(json);
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
