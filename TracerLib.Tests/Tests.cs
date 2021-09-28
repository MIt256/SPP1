using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TracerLib;

namespace TracerTests
{
    [TestClass]
    public class Tests
    {
        const string expectedMethodName = "MethodForTests";
        const string expectedMethodClass = "Tests";
        const int expectedThreadsCount = 3;
        const int expectedMethodsCount = 4;

        Tracer tracer = new Tracer();
        List<Thread> threads = new List<Thread>();

        void MethodForTests()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        [TestMethod]
        public void MethodNameCheck()
        {
            MethodForTests();
            Assert.AreEqual(expectedMethodName, tracer.GetTraceResult().GetThreadTracers()[Thread.CurrentThread.ManagedThreadId].MethodTracerList[0].MethodName);
        }

        [TestMethod]
        public void MethodClassCheck()
        {
            MethodForTests();
            Assert.AreEqual(expectedMethodClass, tracer.GetTraceResult().GetThreadTracers()[Thread.CurrentThread.ManagedThreadId].MethodTracerList[0].ClassName);
        }

        [TestMethod]
        public void ThreadCountCheck()
        {
            for (int i = 0; i < expectedThreadsCount; i++)
            {
                threads.Add(new Thread(MethodForTests));
                threads[i].Start();
                threads[i].Join();
            }

            Assert.AreEqual(expectedThreadsCount, tracer.GetTraceResult().GetThreadTracers().Count);

        }

        [TestMethod]
        public void MethodCountChek()
        {
            for (int i = 0; i < expectedMethodsCount; i++)
            {
                MethodForTests();
            }
            Assert.AreEqual(expectedMethodsCount, tracer.GetTraceResult().GetThreadTracers()[Thread.CurrentThread.ManagedThreadId].MethodTracerList.Count);
        }

        [TestMethod]
        public void MethodTimeIsCorrect()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            MethodForTests();
            var time = stopwatch.ElapsedMilliseconds;

            var methodTime = tracer.GetTraceResult().GetThreadTracers()[Thread.CurrentThread.ManagedThreadId].MethodTracerList[0].Time;

            bool flag = methodTime + 5 >= time;
            flag |= methodTime - 5 <= time;

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void ThreadTimeIsCorrect()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            MethodForTests();
            MethodForTests();
            MethodForTests();
            MethodForTests();
            var time = stopwatch.ElapsedMilliseconds;

            var threadTime = tracer.GetTraceResult().GetThreadTracers()[Thread.CurrentThread.ManagedThreadId].ThreadTime;

            bool flag = threadTime + 5 >= time;
            flag |= threadTime - 5 <= time;

            Assert.IsTrue(flag);
        }

    }
}
