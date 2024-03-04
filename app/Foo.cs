using spp.tracer.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    internal class Foo
    {
        private ITracer tracer;
        private Bar bar;

        public Foo(ITracer tracer)
        {
            this.tracer = tracer;
            bar = new Bar(tracer);
        }

        public void callBarMethods()
        {
            tracer.StartTrace();
            bar.innerTraceMethod();
            bar.simpleTraceMethod();
            bar.anotherThreadSimpleTraceMethod();
            tracer.StopTrace();
        }

        public void simpleFooMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
    }
}