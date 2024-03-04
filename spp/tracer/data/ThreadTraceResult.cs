using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.data
{
    [Serializable]
    public  class ThreadTraceResult
    {
        public long id { get; set; }
        public long time { get; set; }
        public List<MethodTraceResult> methods { get; set; }

        public ThreadTraceResult(long id)
        {
            this.id = id; 
            this.methods = new List<MethodTraceResult>();
        }

        public void updateTime()
        {
            time = 0;
            for(int i = 0; i < methods.Count; i++)
            {
                methods[i].updateTime();
                time += methods[i].duration;
            }
        }
    }
}
