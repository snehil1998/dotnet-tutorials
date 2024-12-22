namespace Multithreading;

public static class Join {
    public static void Implement() {
        Console.WriteLine("Main Thread has started");
        Thread t1 = new Thread(Method1);
        t1.Start();
        Thread t2 = new Thread(Method2);
        t2.Start();

        // t1.Join(); // we are making the main thread to wait until the child thread t1 completes
        // Console.WriteLine("Method 1 execution completed");

        // example with timeout. It will make main thread wait only for 2 secs
        if(t1.Join(2000)) 
        {
            Console.WriteLine("Method 1 execution completed");
        }

        t2.Join(); // we are making the main thread to wait until the child thread t2 completes
        Console.WriteLine("Method 2 execution completed");

        if(t1.IsAlive)
        {
            Console.WriteLine("Method 1 execution is still going on");
        }
        else
        {
            Console.WriteLine("Method 1 execution has completed");
        }

        Console.WriteLine("Main thread ended");
        Console.ReadLine();
    }

    private static void Method1() {
        Console.WriteLine("Method 1 execution started");
        Thread.Sleep(3000);
        Console.WriteLine("Method 1 is awake");  
    }

    private static void Method2() {
        Console.WriteLine("Method 2 execution started");
    }
}