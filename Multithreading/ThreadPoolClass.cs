namespace Multithreading;

public static class ThreadPoolClass
{
    public static void Implement()
    {
        Console.WriteLine("Main method has started");
        for(int i=0; i< 5; i++)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SomeMethod)); //this method generated a thread from the thread pool as and when required to execute the callback method
        }
        Console.WriteLine("Main method has ended");
        Console.ReadLine();
    }

    private static void SomeMethod(object? obj)
    {
        Thread thread = Thread.CurrentThread;
        string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
        Console.WriteLine(message);
    }
}
