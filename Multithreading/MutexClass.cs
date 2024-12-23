namespace Multithreading;

public class MutexClass {
    private static Mutex _event = new Mutex();
    public static void Implement()
    {
        for(int i=0; i<5; i++)
        {
            new Thread(Write).Start();
        }

        // if we try to release mutex here, we get an exception.
        // At a time only one thread can manage the release of a mutex. Here we are trying to release from main thread
        // Thread.Sleep(4000);
        // _event.ReleaseMutex();

        Console.ReadLine();
    }
    private static void Write()
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Write thread waiting... ");
        if(_event.WaitOne())
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Write thread working... ");
            Thread.Sleep(5000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Write thread completed... ");
            _event.ReleaseMutex();
        }
        else
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Could not execute lock... ");
        }
    }
}