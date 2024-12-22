namespace Multithreading;

public class ManualResetEventClass {
    private static ManualResetEvent _mre = new ManualResetEvent(false); // when false, the read thread is supposed to wait
    public static void Implement()
    {
        Thread t1 = new Thread(Write);
        t1.Start();
        for(int i=0; i<5; i++)
        {
            new Thread(Read).Start();
        }

        Console.ReadLine();
    }
    private static void Write()
    {
        Console.WriteLine("Write thread working... ");
        _mre.Reset(); // sets to false
        Thread.Sleep(5000);
        Console.WriteLine("Write thread completed... ");
        _mre.Set(); // sets to true
    }

    private static void Read()
    {
        Console.WriteLine("Read thread wait... ");
        _mre.WaitOne(); // wait if the MRE value is false, until write finishes and sets MRE to true
        Console.WriteLine("Read thread completed... ");
    }
}