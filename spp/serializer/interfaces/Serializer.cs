using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp.serializer.interfaces
{
    public interface Serializer<T>
    {
        string serialize(T obj);
    }
}
