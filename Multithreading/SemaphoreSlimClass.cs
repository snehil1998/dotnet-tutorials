using System;
using System.IO.Compression;

namespace Multithreading;

public static class SemaphoreSlimClass
{
    private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(3);
    private static SemaphoreSlim _semaphoreSlimTask = new SemaphoreSlim(3, 3);

    public static void Implement()
    {
        Console.WriteLine("Main thread starts");
        // run threads
        // for(int i=0; i<5; i++)
        // {
        //     Thread t = new Thread(() => SemaphoreSlimMethod((i+1)*100)) {
        //         Name = "Thread " + i
        //     };
        //     t.Start();
        // }

        // run tasks
        Task[] tasks = new Task[5];
        for(int i=0; i<5; i++)
        {
            tasks[i] =  Task.Run(() => {
                SemaphoreSlimTask(i+1000);
            });
        }
        Thread.Sleep(1000); //wait for tasks to initialise
        Task.WhenAll(tasks);

        Console.WriteLine("Main thread ends");
        Console.ReadLine();
    }

    private static void SemaphoreSlimMethod(int delay)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} waits to access resource");
        _semaphoreSlim.Wait();
        Console.WriteLine($"{Thread.CurrentThread.Name} was granted access to the resource");
        Thread.Sleep(delay);
        Console.WriteLine($"{Thread.CurrentThread.Name} task was completed");
        _semaphoreSlim.Release();
    }

    private static void SemaphoreSlimTask(int delay)
    {
        try{
            Console.WriteLine($"{Task.CurrentId} waits to access resource");
            _semaphoreSlimTask.Wait();
            Console.WriteLine($"{Task.CurrentId} was granted access to the resource");
            Thread.Sleep(delay);
        }
        finally
        {
            Console.WriteLine($"{Task.CurrentId} task was completed");
            _semaphoreSlimTask.Release();
        }
    }
}
