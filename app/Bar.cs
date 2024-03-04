using spp.tracer.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    internal class Bar
    {
        private ITracer tracer;

        public Bar(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void simpleTraceMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(500);
            tracer.StopTrace();
        }

        public void anotherThreadSimpleTraceMethod()
        {
            tracer.StartTrace();
            Thread thread = new Thread(simpleTraceMethod);
            thread.Start();
            Thread.Sleep(300);
            tracer.StartTrace();
        }

        public void innerTraceMethod()
        {
            tracer.StartTrace();
            simpleTraceMethod();
            tracer.StopTrace();
        }
    }
}
