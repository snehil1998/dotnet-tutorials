namespace Multithreading;

public class AsyncAwait {
    public static async Task Implement() {
        Console.WriteLine("Main Thread has started");
        var total = await Method(); // calling method suspends until this task is completed. Then continues execution
        var set = new HashSet<int>() {1, 1};
        Console.WriteLine(set.Count);
        Console.WriteLine("Main thread ended with result: " + total);
        Console.ReadLine();
    }

    private static async Task<int> Method() {
        Console.WriteLine("Method: Started execution using " + Thread.CurrentThread.ManagedThreadId);
        int Sum = 0;
        for(var i=0; i< 5; i++) {
            Sum += i;
        }
        // awaits on another process which takes 5 seconds. We should await for a task only if we are using the results in the future,
        // otherwise we can just let the child thread continue executing in the background and continue the parent thread with process
        await Task.Delay(5000);
        Console.WriteLine("Method: Ended execution");
        return Sum;
    }
}