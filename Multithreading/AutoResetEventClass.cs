namespace Multithreading;

public class AutoResetEventClass {
    private static AutoResetEvent _event = new AutoResetEvent(true); // when false, the read thread is supposed to wait at WaitOne()
    public static void Implement()
    {
        for(int i=0; i<5; i++)
        {
            new Thread(Write).Start();
        }

        // if we sleep and set auto reset event in the main funtion, it can lead to multiple threads executing tgt. This can be solved by Mutex.
        Thread.Sleep(4000);
        _event.Set();

        Console.ReadLine();
    }
    private static void Write()
    {
        Console.WriteLine("Write thread waiting... ");
        _event.WaitOne();
        Console.WriteLine("Write thread working... ");
        Thread.Sleep(5000);
        Console.WriteLine("Write thread completed... ");
        _event.Set();
    }
}