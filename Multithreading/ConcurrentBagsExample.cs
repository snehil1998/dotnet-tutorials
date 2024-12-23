using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Multithreading;

public static class ConcurrentBagsExample
{
    private static ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
    private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

    public static void Implement()
    {
        // both threads first complete adding items and then the producer-consumer thread reads the results.
        // In the bag 50, 60, 70 is added after 40, 30, 20, 10 but task1 is accessing items so it gets preference
        Task task1 = Task.Run(() => ProducerAndConsumer());
        Task task2 = Task.Run(() => Producer());
        Task.WaitAll(task1, task2);
    }

    private static void ProducerAndConsumer()
    {
        int[] arr = { 10, 20, 30, 40 };
        foreach(var num in arr)
        {
            concurrentBag.Add(num);
        }

        autoResetEvent.WaitOne();

        foreach(var num in concurrentBag)
        {
            Console.WriteLine(num);
        }

    }

    private static void Producer()
    {
        int[] arr = { 50, 60, 70 };
        foreach(var num in arr)
        {
            concurrentBag.Add(num);
        }
        autoResetEvent.Set();
    }
}
