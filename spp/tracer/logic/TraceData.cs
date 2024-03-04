using spp.tracer.data;
using spp.tracer.exception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.logic
{
    internal class TraceData
    {
        private ConcurrentDictionary<long, Stack<MethodTraceResult>> methodTraceResultsByThread;
        private ConcurrentDictionary<long, List<MethodTraceResult>> topLevelMethodTraceResultsByThread;

        public TraceData()
        {
            this.methodTraceResultsByThread = new ConcurrentDictionary<long, Stack<MethodTraceResult>>();
            this.topLevelMethodTraceResultsByThread = new ConcurrentDictionary<long, List<MethodTraceResult>>();
        }

        public void addMethodTraceResult(MethodTraceResult methodTraceResult, long threadId)
        {
            Stack<MethodTraceResult> methodStack;
            if (!methodTraceResultsByThread.ContainsKey(threadId))
            {
                methodStack = new Stack<MethodTraceResult>();
                methodTraceResultsByThread[threadId] = methodStack;
                if (!topLevelMethodTraceResultsByThread.ContainsKey(threadId))
                {
                    topLevelMethodTraceResultsByThread.TryAdd(threadId, new List<MethodTraceResult>());
                }
                topLevelMethodTraceResultsByThread[threadId].Add(methodTraceResult);
            }
            else
            {
                methodStack = methodTraceResultsByThread[threadId];
                if (methodStack.Count > 0)
                {
                    methodStack.Peek().addChildMethod(methodTraceResult);
                }
                else
                {
                    topLevelMethodTraceResultsByThread[threadId].Add(methodTraceResult);
                }
            }
            methodStack.Push(methodTraceResult);
        }

        public void stopTrace()
        {
            long threadId = Thread.CurrentThread.ManagedThreadId;
            if (!methodTraceResultsByThread.ContainsKey(threadId) || methodTraceResultsByThread[threadId].Count == 0)
            {
                throw new NoCorrespondingStartTraceException();
            }
            MethodTraceResult methodTraceResult = methodTraceResultsByThread[threadId].Pop();
            methodTraceResult.updateTime();
            methodTraceResult.isFinished = true;
            
        }

        public TraceResult getResult()
        {
            List<ThreadTraceResult> threads = new List<ThreadTraceResult>();
            foreach (KeyValuePair<long, List<MethodTraceResult>> kvp in topLevelMethodTraceResultsByThread)
            {
                ThreadTraceResult threadTraceResult = new ThreadTraceResult(kvp.Key);
                threadTraceResult.methods = kvp.Value;
                threads.Add(threadTraceResult);
                threadTraceResult.updateTime();
            }
            return new TraceResult(threads);
        }
    }
}
