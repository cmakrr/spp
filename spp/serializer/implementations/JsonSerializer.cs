using Newtonsoft.Json;
using spp.serializer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace spp.serializer.implementations
{
    public class JsonSerializer<T> : Serializer<T>
    {
        public string serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
