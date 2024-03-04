using spp.serializer.implementations;
using spp.tracer.data;
using spp.tracer.logic;


public class TestClass
{
    static JsonSerializer<TraceResult> serializer = new JsonSerializer<TraceResult>();
    static Tracer tracer = new Tracer();
    public static void Main(string[] args)
    {
        lol();
        Thread.Sleep(1000);
        Console.WriteLine(serializer.serialize(tracer.GetTraceResult()));
    }

    public static void lol()
    {
        tracer.StartTrace();
        kek();
        tracer.StopTrace();
    }

    public static void kek()
    {
        tracer.StartTrace();
        Thread thread = new Thread(check);
        thread.Start();
        Thread.Sleep(300);
        tracer.StopTrace();
    }

    public static void check()
    {
        tracer.StartTrace();
        Thread.Sleep(500);
        tracer.StopTrace();
    }
}
