using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Multithreading;

public static class ConcurrentQueues
{
    private static ConcurrentQueue<string> concurrentQueue = new ConcurrentQueue<string>();

    private static object _lock = new object();

    public static void Implement()
    {
        Stopwatch stopwatch = new Stopwatch();
        var queue = new Queue<string>();

        // sync
        // queue.Clear();
        // AddSync("Rohan");
        // AddSync("Rahul");
        // foreach(var str in queue)
        // {
        //     Console.WriteLine(str);
        // }

        //async - When you run multiple times, you will get 
        // ArgumentException: Destination array was not long enough. Check the destination index, length, and the array's lower bounds
        // at some point. Generic Queue is not thread safe which makes it unpredictable
        // queue.Clear();
        // Task Rohan = Task.Run(() => AddSync("Rohan", queue));
        // Task Rahul = Task.Run(() => AddSync("Rahul", queue));
        // Task.WaitAll(Rohan, Rahul);
        // foreach(var str in queue)
        // {
        //     Console.WriteLine(str);
        // }

        //locking - resolves the above inconsistency and exception
        // queue.Clear();
        // stopwatch.Start();
        // Task Rohan = Task.Run(() => AddLocked("Rohan", queue));
        // Task Rahul = Task.Run(() => AddLocked("Rahul", queue));
        // Task.WaitAll(Rohan, Rahul);
        // stopwatch.Stop();
        // Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        // foreach(var str in queue)
        // {
        //     Console.WriteLine(str);
        // }


        //concurrent queue - does the same thing more efficiently
        concurrentQueue.Clear();
        stopwatch.Start();
        Task Rohan = Task.Run(() => AddConcurrent("Rohan", concurrentQueue));
        Task Rahul = Task.Run(() => AddConcurrent("Rahul", concurrentQueue));
        Task.WaitAll(Rohan, Rahul);
        stopwatch.Stop();
        Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        foreach(var str in concurrentQueue)
        {
            Console.WriteLine(str);
        }

    }

    private static void AddSync(string name, Queue<string> queue)
    {
        for(int i=1; i<=3; i++)
        {
            queue.Enqueue($"{name} has {i} orders");
        }
    }

    private static void AddLocked(string name, Queue<string> queue)
    {
        lock (_lock)
        {
            for(int i=1; i<=3; i++)
            {
                queue.Enqueue($"{name} has {i} orders");
            }
        }
    }

    private static void AddConcurrent(string name, ConcurrentQueue<string> queue)
    {
        for(int i=1; i<=3; i++)
        {
            queue.Enqueue($"{name} has {i} orders");
        }
    }
}
