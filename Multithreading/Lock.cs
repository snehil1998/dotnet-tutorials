using System.Diagnostics;

namespace Multithreading;

public static class Lock {
    private static int Sum = 0;
    public static void Implement() {
        Console.WriteLine("Main method execution started");

        Stopwatch watch = Stopwatch.StartNew();

        Thread t1 = new Thread(Addition);
        Thread t2 = new Thread(Addition);
        Thread t3 = new Thread(Addition);

        // without locking it will lead to inconsistent sum everytime we run this coz all three threads try to access method and update sum simultaneously
        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine("Total sum is " + Sum);

        watch.Stop();
        Console.WriteLine("Total tick time is: " + watch.ElapsedTicks);

        Console.WriteLine("Main method execution has completed");
        Console.ReadLine();
    }

    private static void Addition() {
        for(int i=1; i<50000; i++) 
        {
            // Sum++; // can lead to inconsistent output due to all threads try to update it simultaneously

            // if we use this we will get consistent output, enables locking between threads. Interlocked is slightly performance efficient compared to lock but we can only do increments 
            // Interlocked.Increment(ref Sum);

            // it locks the current thread until the execution is completed. The other thread will wait until previous thread completes execution
            // lock(_lock)
            // {
            //     Sum++;
            // }

            // we can also implement locking using Monitor class using .Enter() and .Exit(), and also other advanced multithreading mechanisms such as wait, pulse, pulseAll, etc.
            bool lockTaken = false;
            Monitor.Enter(_lock, ref lockTaken);
            try
            {
                Sum++;
            }
            finally
            {
                if(lockTaken) {
                    Monitor.Exit(_lock);
                }
            }
        }
    }

    public static object _lock = new object();
}