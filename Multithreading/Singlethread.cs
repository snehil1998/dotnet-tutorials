namespace Multithreading;

public static class Singlethread {
    public static void Implement() {
        Console.WriteLine("Main Thread has started");
        //execute methods
        Method1();
        Method2();
        Method3();
        Console.WriteLine("Main thread ended");
        Console.ReadLine();
    }

    private static void Method1() {
        Console.WriteLine("Method 1: Started execution using " + Thread.CurrentThread.Name);
        for(var i=0; i< 5; i++) {
            Console.WriteLine($"Method 1: {i}");
        }
        Console.WriteLine("Method 1: Ended execution");
    }

    private static void Method2() {
        Console.WriteLine("Method 2: Started execution using " + Thread.CurrentThread.Name);
        for(var i=0; i< 5; i++) {
            Console.WriteLine($"Method 2: {i}");
            if (i == 2) {
                Console.WriteLine("Method 2: Started DB operation");
                Thread.Sleep(5000);
                Console.WriteLine("Method 2: Ended DB operation");
            }
        }
        Console.WriteLine("Method 2: Ended execution");
    }

    private static void Method3() {
        Console.WriteLine("Method 3: Started execution using " + Thread.CurrentThread.Name);
        for(var i=0; i< 5; i++) {
            Console.WriteLine($"Method 3: {i}");
        }
        Console.WriteLine("Method 3: Ended execution");
    }
}