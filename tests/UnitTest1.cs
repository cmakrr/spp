using NUnit.Framework;
using spp.tracer.data;
using spp.tracer.exception;
using spp.tracer.logic;
using System.Reflection;
using System.Threading;

namespace tests
{
    [TestFixture]
    public class Tests
    {
        private Tracer tracer;

        [SetUp]
        public void SetUp()
        {
            tracer = new Tracer();
        }

        [Test]
        public void GivenNoStartTrace_WhenStopTrace_ThenThrowException()
        {
            Assert.Throws<NoCorrespondingStartTraceException>(() => tracer.StopTrace());
            
        }

        [Test]
        public void GivenOneMethod_WhenTrace_ThenReturnResult()
        {
            string expectedMethodName= MethodBase.GetCurrentMethod().Name;
            long threadId = Thread.CurrentThread.ManagedThreadId;

            tracer.StartTrace();
            tracer.StopTrace();

            TraceResult result = tracer.GetTraceResult();

            Assert.AreEqual(1, result.threads.Count);
            Assert.AreEqual(threadId, result.threads[0].id);
            Assert.AreEqual(1, result.threads[0].methods.Count);
            Assert.AreEqual(expectedMethodName, result.threads[0].methods[0].methodName);
            Assert.True(result.threads[0].methods[0].isFinished);
        }

        [Test]
        public void GivenOneMethodWithoutStopTrace_WhenTrace_ThenReturnResult()
        {
            string expectedMethodName = MethodBase.GetCurrentMethod().Name;
            long threadId = Thread.CurrentThread.ManagedThreadId;

            tracer.StartTrace();

            TraceResult result = tracer.GetTraceResult();

            Assert.AreEqual(1, result.threads.Count);
            Assert.AreEqual(threadId, result.threads[0].id);
            Assert.AreEqual(1, result.threads[0].methods.Count);
            Assert.AreEqual(expectedMethodName, result.threads[0].methods[0].methodName);
            Assert.IsFalse(result.threads[0].methods[0].isFinished);
        }

        [Test]
        public void GivenInnerMethod_WhenTrace_ThenReturnResult()
        {
            string expectedMethodName = MethodBase.GetCurrentMethod().Name;
            long threadId = Thread.CurrentThread.ManagedThreadId;
            int expectedMethodsCount = 1;
            string secondMethodName = "innerMethod";

            tracer.StartTrace();
            innerMethod();
            tracer.StopTrace();

            TraceResult result = tracer.GetTraceResult();

            Assert.AreEqual(1, result.threads.Count);
            Assert.AreEqual(threadId, result.threads[0].id);
            Assert.AreEqual(expectedMethodsCount, result.threads[0].methods.Count);
            Assert.AreEqual(expectedMethodName, result.threads[0].methods[0].methodName);
            Assert.True(result.threads[0].methods[0].isFinished);
            MethodTraceResult actualInnerMethod;
            result.threads[0].methods[0].childMethods.TryPeek(out actualInnerMethod);
            Assert.AreEqual(secondMethodName, actualInnerMethod.methodName);
            Assert.True(actualInnerMethod.isFinished);
        }

        private void innerMethod()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }
    }
}