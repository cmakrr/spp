using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.data
{
    [Serializable]
    public class MethodTraceResult
    {
        private long startTime { get; set; }
        public string methodName { get; }
        public long duration { get; private set; }
        public bool isFinished { get; set; }
        public ConcurrentQueue<MethodTraceResult> childMethods { get; set; }

        public MethodTraceResult(string methodName)
        {
            this.methodName = methodName;
            this.childMethods = new ConcurrentQueue<MethodTraceResult>();
            this.startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public void updateTime()
        {
            if (!isFinished)
            {
                foreach(var methodTraceResult in this.childMethods)
                {
                    methodTraceResult.updateTime();
                }
                duration = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime;
            }
        }

        public void addChildMethod(MethodTraceResult childMethod)
        {
            childMethods.Enqueue(childMethod);
        }
    }
}
