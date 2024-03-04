using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.data
{
    [Serializable]
    public class TraceResult
    {
        public List<ThreadTraceResult> threads;

        public TraceResult(List<ThreadTraceResult> threads)
        {
            this.threads = threads; 
        }
    }
}
