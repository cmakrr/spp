using spp.tracer.data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.logic
{
    public class Tracer : ITracer
    {
        private TraceData traceData = new TraceData();

        public TraceResult GetTraceResult()
        {
            return traceData.getResult();
        }

        public void StartTrace()
        {
            long threadId = Thread.CurrentThread.ManagedThreadId;
            string methodName = getCallingMethodName();
            MethodTraceResult methodTraceResult = new MethodTraceResult(methodName);
            traceData.addMethodTraceResult(methodTraceResult, threadId);
        }

        private string getCallingMethodName()
        {
            StackTrace st = new(2);
            StackFrame? sf = st.GetFrame(0);
            if (sf == null)
            {
                throw new NullReferenceException();
            }

            MethodBase? mb = sf.GetMethod();
            if (mb == null)
            {
                throw new NullReferenceException();
            }
            return mb.Name;
        }

        public void StopTrace()
        {
            traceData.stopTrace();
        }
    }
}
