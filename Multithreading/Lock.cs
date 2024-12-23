using System.Diagnostics;

namespace Multithreading;

public static class Lock {
    private static int Sum = 0;
    public static void Implement() {
        Console.WriteLine("Main method execution started");

        Stopwatch watch = Stopwatch.StartNew();

        // without locking it will lead to inconsistent sum everytime we run this coz all three threads try to access method and update sum simultaneously
        Thread t1 = new Thread(Addition);
        Thread t2 = new Thread(Addition);
        Thread t3 = new Thread(Addition);

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

    public static void ImplementTryEnter() {
        Console.WriteLine("Main method execution started");

        // example for try enter function, where a thread should not acquire lock coz it times out (exceeds 1sec)
        Thread t1 = new Thread(TryEnterMethod) {
            Name = "Thread1"
        };
        Thread t2 = new Thread(TryEnterMethod) {
            Name = "Thread2"
        };
        Thread t3 = new Thread(TryEnterMethod) {
            Name = "Thread3"
        };

        t1.Start();
        t2.Start();
        t3.Start();

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

    private static void TryEnterMethod() {
        bool lockTaken = false;
        TimeSpan milliseconds = TimeSpan.FromMilliseconds(1000); // 1 sec
        try
        {
            Monitor.TryEnter(_lock, milliseconds, ref lockTaken);
            if(lockTaken)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock");
                for(int i=0; i<5; i++) 
                {
                    Thread.Sleep(100);
                }
                Console.WriteLine($"{Thread.CurrentThread.Name} executed locked code");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} failed to acquire lock within the timeout");
            }
        }
        finally
        {
            if(lockTaken) {
                Monitor.Exit(_lock);
                Console.WriteLine($"{Thread.CurrentThread.Name} exited from the lock");
            }
        }
    }

    public static object _lock = new object();
}