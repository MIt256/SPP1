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

            var xmlSerializer = new LXmlSerializer();
            var jsonSerializer = new JsonSerializer();
            TraceResult traceResult = tracer.GetTraceResult();
            string json = jsonSerializer.Serialize(traceResult);
            string xml = xmlSerializer.Serialize(traceResult);

            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var dataPath = Path.Combine(projectDirectory, "Files");

            var filePrinter = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.xml")));
            var consolePrinter = new ConsolePrinter();

            filePrinter.PrintResult(xml);
            consolePrinter.PrintResult(xml);

            filePrinter = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.json")));

            filePrinter.PrintResult(json);
            consolePrinter.PrintResult(json);
        }

        public void Method(object o)
        {
            Tracer tracer = (Tracer) o;
            tracer.StartTrace();
            Thread.Sleep(50);
            tracer.StopTrace();
        }
    }
}
