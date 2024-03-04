using spp.serializer.implementations;
using spp.tracer.data;
using spp.tracer.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    internal class TestClass
    {
        private const string filePath = "C:\\Users\\gg\\Desktop\\Resources\\spp\\app\\file.txt";

        public static void Main(string[] args)
        {
            JsonSerializer<TraceResult> serializer = new JsonSerializer<TraceResult>();
            Tracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.simpleFooMethod();
            foo.callBarMethods();
            Thread.Sleep(500);
            string json = serializer.serialize(tracer.GetTraceResult());
            Console.WriteLine(json);
            File.WriteAllText(filePath, json);
        }
    }
}
