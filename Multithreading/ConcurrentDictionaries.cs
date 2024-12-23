using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Multithreading;

public static class ConcurrentDictionaries
{
    private static ConcurrentDictionary<string, string> concurrentDictionary = new();
    private static Dictionary<string, string> dictionary = new();

    private static object _lock = new object();

    public static void Implement()
    {
        Stopwatch stopwatch = new Stopwatch();
        dictionary.Clear();
        dictionary.Add("UK", "United Kingdom");
        dictionary.Add("US", "United States");
        concurrentDictionary.TryAdd("UK", "United Kingdom");
        concurrentDictionary.TryAdd("US", "United States");

        // generic dictionary - adding duplicate keys will cause exception
        // AddSync("UK", "United Kingdom Updated");

        // concurrent dictionary - adding duplicate keys will not update nor cause an exception
        // AddConcurrent("UK", "United Kingdom Updated");
        // Console.WriteLine(concurrentDictionary["UK"]);
        // concurrentDictionary.AddOrUpdate("US", "United States", (k, v) => "United States Updated"); // if key exists, should update with the updated value
        // Console.WriteLine(concurrentDictionary["US"]);
        // concurrentDictionary.AddOrUpdate("IND", "India", (k, v) => "India Updated"); // if key does not exist, add key-value pair
        // Console.WriteLine(concurrentDictionary["IND"]);
        // concurrentDictionary.TryUpdate("UK", "United Kingdom Updated", "United Kingdom"); //if key exists and comparisonValue matches, update with newValue
        // Console.WriteLine(concurrentDictionary["UK"]);

        // when tasks passed to generic dictionary - should give inconsistent results
        // Task task1 = Task.Run(() => AddSync("SL", "Sri Lanka 1"));
        // Task task2 = Task.Run(() => AddSync("SL", "Sri Lanka 2"));
        // Task.WaitAll(task1, task2);
        // Console.WriteLine(dictionary["SL"]);

        // when tasks passed to generic dictionary - should cause exception
        // stopwatch.Start();
        // Task task1 = Task.Run(() => AddLocked("SL", "Sri Lanka 1"));
        // Task task2 = Task.Run(() => AddLocked("SL", "Sri Lanka 2"));
        // Task.WaitAll(task1, task2);
        // stopwatch.Stop();
        // Console.WriteLine($"{stopwatch.ElapsedTicks}");
        // Console.WriteLine(dictionary["SL"]);

        // when tasks passed to concurrent dictionary - should not give exception and values can differ based on order of thread execution.
        // Also more efficient compared to locks
        stopwatch.Start();
        Task task1 = Task.Run(() => AddConcurrent("SL", "Sri Lanka 1"));
        Task task2 = Task.Run(() => AddConcurrent("SL", "Sri Lanka 2"));
        stopwatch.Stop();
        Task task3 = Task.Run(() => AddConcurrent("NZ", "New Zealand 1"));
        Task task4 = Task.Run(() => AddConcurrent("NZ", "New Zealand 2"));
        Task task5 = Task.Run(() => AddConcurrent("AUS", "Australia 1"));
        Task task6 = Task.Run(() => AddConcurrent("AUS", "Australia 2"));
        Task task7 = Task.Run(() => AddConcurrent("PAK", "Pakistan 1"));
        Task task8 = Task.Run(() => AddConcurrent("PAK", "Pakistan 2"));
        Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8);
        Console.WriteLine($"{stopwatch.ElapsedTicks}");
        foreach(var pair in concurrentDictionary)
            Console.WriteLine($"{pair.Key}, {pair.Value}");

    }

    private static void AddSync(string name, string fullName)
    {
        dictionary.Add(name, fullName);
    }

    private static void AddLocked(string name, string fullName)
    {
        lock (_lock)
        {
            dictionary.TryAdd(name, fullName);
        }
    }

    private static void AddConcurrent(string name, string fullName)
    {
        for(int i=1; i<=3; i++)
        {
            concurrentDictionary.TryAdd(name, fullName);
        }
    }
}
