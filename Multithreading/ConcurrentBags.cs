using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Multithreading;

public static class ConcurrentBags
{
    private static ConcurrentBag<string> concurrentBag = new ConcurrentBag<string>();

    private static object _lock = new object();

    public static void Implement()
    {
        Stopwatch stopwatch = new Stopwatch();
        var list = new List<string>();
        list.Clear();
        concurrentBag.Clear();

        // sync
        // AddSync("Rohan", list);
        // AddSync("Rahul", list);
        // foreach(var str in list)
        // {
        //     Console.WriteLine(str);
        // }

        //async - When you run multiple times, you will get inconsistent results
        // Task Rohan = Task.Run(() => AddSync("Rohan", list));
        // Task Rahul = Task.Run(() => AddSync("Rahul", list));
        // Task.WaitAll(Rohan, Rahul);
        // foreach(var str in list)
        // {
        //     Console.WriteLine(str);
        // }

        //locking - resolves the above inconsistency
        // stopwatch.Start();
        // Task Rohan = Task.Run(() => AddLocked("Rohan", list));
        // Task Rahul = Task.Run(() => AddLocked("Rahul", list));
        // Task.WaitAll(Rohan, Rahul);
        // stopwatch.Stop();
        // Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        // foreach(var str in list)
        // {
        //     Console.WriteLine(str);
        // }

        //concurrent queue - does the same thing more efficiently. gives priority in reverse order
        stopwatch.Start();
        Task Rohan = Task.Run(() => AddConcurrent("Rohan", concurrentBag));
        Task Rahul = Task.Run(() => AddConcurrent("Rahul", concurrentBag));
        Task.WaitAll(Rohan, Rahul);
        stopwatch.Stop();
        Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        foreach(var str in concurrentBag)
        {
            Console.WriteLine(str);
        }
    }

    private static void AddSync(string name, List<string> list)
    {
        for(int i=1; i<=3; i++)
        {
            list.Add($"{name} has {i} orders");
        }
    }

    private static void AddLocked(string name, List<string> list)
    {
        lock (_lock)
        {
            for(int i=1; i<=3; i++)
            {
                list.Add($"{name} has {i} orders");
            }
        }
    }

    private static void AddConcurrent(string name, ConcurrentBag<string> bag)
    {
        for(int i=1; i<=3; i++)
        {
            bag.Add($"{name} has {i} orders");
        }
    }
}
