using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Multithreading;

public static class ConcurrentStacks
{
    private static ConcurrentStack<string> concurrentStack = new ConcurrentStack<string>();

    private static object _lock = new object();

    public static void Implement()
    {
        Stopwatch stopwatch = new Stopwatch();
        var stack = new Stack<string>();
        stack.Clear();
        concurrentStack.Clear();

        // sync
        // AddSync("Rohan", stack);
        // AddSync("Rahul", stack);
        // foreach(var str in stack)
        // {
        //     Console.WriteLine(str);
        // }

        //async - When you run multiple times, you will get inconsistent results
        // Task Rohan = Task.Run(() => AddSync("Rohan", stack));
        // Task Rahul = Task.Run(() => AddSync("Rahul", stack));
        // Task.WaitAll(Rohan, Rahul);
        // foreach(var str in stack)
        // {
        //     Console.WriteLine(str);
        // }

        //locking - resolves the above inconsistency
        // stopwatch.Start();
        // Task Rohan = Task.Run(() => AddLocked("Rohan", stack));
        // Task Rahul = Task.Run(() => AddLocked("Rahul", stack));
        // Task.WaitAll(Rohan, Rahul);
        // stopwatch.Stop();
        // Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        // foreach(var str in stack)
        // {
        //     Console.WriteLine(str);
        // }


        //concurrent queue - does the same thing more efficiently
        stopwatch.Start();
        Task Rohan = Task.Run(() => AddConcurrent("Rohan", concurrentStack));
        Task Rahul = Task.Run(() => AddConcurrent("Rahul", concurrentStack));
        Task.WaitAll(Rohan, Rahul);
        stopwatch.Stop();
        Console.WriteLine($"Time elapsed: {stopwatch.ElapsedTicks}");
        foreach(var str in concurrentStack)
        {
            Console.WriteLine(str);
        }

    }

    private static void AddSync(string name, Stack<string> stack)
    {
        for(int i=1; i<=3; i++)
        {
            stack.Push($"{name} has {i} orders");
        }
    }

    private static void AddLocked(string name, Stack<string> stack)
    {
        lock (_lock)
        {
            for(int i=1; i<=3; i++)
            {
                stack.Push($"{name} has {i} orders");
            }
        }
    }

    private static void AddConcurrent(string name, ConcurrentStack<string> stack)
    {
        for(int i=1; i<=3; i++)
        {
            stack.Push($"{name} has {i} orders");
        }
    }
}
