namespace Multithreading;

public static class PulseAndWait {
    private static object _lock = new();
    public static void Implement() {
        Thread t1 = new Thread(Write);
        Thread t2 = new Thread(Read);

        // since they are alternatingly notifying and receiving notification, both threads get executed loopwise
        // i.e., first loop 1 in both thread 1 and 2 execute, then loop 2, then loop 3, and so on...
        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.ReadLine();
    }

    private static void Write()
    {
        Monitor.Enter(_lock); 
        for(int i=0; i<5; i++)
        {
            Monitor.Pulse(_lock); // notifies thread 2 to start implementing its method
            Console.WriteLine("Write thread working... " + i);

            Console.WriteLine("Write thread completed... " + i);
            Monitor.Wait(_lock); // waits for the notification from thread 2
        }
    }

    private static void Read()
    {
        Monitor.Enter(_lock);
        for(int i=0; i<5; i++)
        {
            Monitor.Pulse(_lock); // notifies thread 1 to start implementing its method
            Console.WriteLine("Read thread working... " + i);

            Console.WriteLine("Read thread completed... " + i);
            Monitor.Wait(_lock); // waits for the notification from thread 1
        }
    }

}