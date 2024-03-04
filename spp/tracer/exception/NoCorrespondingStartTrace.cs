using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.tracer.exception
{
    public class NoCorrespondingStartTraceException : Exception
    {
        public NoCorrespondingStartTraceException() : 
            base("You tried to call stop trace without corresponding start trace call")
        {
        }
    }
}
