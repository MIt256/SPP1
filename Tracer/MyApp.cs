using App.Output;
using System;
using System.IO;
using System.Threading;
using TracerLib;
using TracerLib.Serialization;

namespace MyTracerApp
{
    class MyApp
    {
        static void Main(string[] args)
        {
            //main thread
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
            //methods from task
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            //new thread
            MyApp app = new MyApp();
            Thread thread1 = new Thread(new ParameterizedThreadStart(app.MethodForThread));
            thread1.Start(tracer);
            thread1.Join();
            //get results and serialize
            TraceResult traceResult = tracer.GetTraceResult();
            var xmlSerializer = new LXmlSerializer();
            var jsonSerializer = new JsonSerializer();
            string json = jsonSerializer.Serialize(traceResult);
            string xml = xmlSerializer.Serialize(traceResult);
            //get path to files
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var dataPath = Path.Combine(projectDirectory, "OutputFiles");

            var filePrinterXML = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.xml")));
            var consolePrinter = new ConsolePrinter();
            var filePrinterJSON = new FilePrinter(Path.GetFullPath(Path.Combine(dataPath, "trace.json")));

            filePrinterXML.PrintResult(xml);
            consolePrinter.PrintResult(xml);
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////////");
            filePrinterJSON.PrintResult(json);
            consolePrinter.PrintResult(json);
        }

        public class Bar
        {
            private ITracer _tracer;

            internal Bar(ITracer tracer)
            {
                _tracer = tracer;
            }

            public void InnerMethod()
            {
                _tracer.StartTrace();
                Thread.Sleep(100);
                _tracer.StopTrace();
            }
        }
        public class Foo
        {
            private Bar _bar;
            private ITracer _tracer;

            internal Foo(ITracer tracer)
            {
                _tracer = tracer;
                _bar = new Bar(_tracer);
            }

            public void MyMethod()
            {
                _tracer.StartTrace();
                _bar.InnerMethod();
                _tracer.StopTrace();
            }
        }
        public void MethodForThread(object o)
        {
            Tracer tracer = (Tracer)o;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

    }
}
