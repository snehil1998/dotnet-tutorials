namespace Multithreading;

public class SemaphoreClass {
    private static Semaphore _event = new Semaphore(2,2); //initial and max threads working. Initial <= max else throws an exception
    public static void Implement()
    {
        for(int i=0; i<5; i++)
        {
            new Thread(Write).Start();
        }

        Console.ReadLine();
    }
    private static void Write()
    {
        Console.WriteLine("Write thread waiting... ");
        _event.WaitOne();
        Console.WriteLine("Write thread working... ");
        Thread.Sleep(5000);
        Console.WriteLine("Write thread completed... ");
        _event.Release();
    }
}