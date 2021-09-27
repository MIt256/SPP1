using System;
using System.Threading;
using TracerLib;

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
            Console.WriteLine(tracer.GetTraceResult()); 
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
