using Multithreading.DeadlockExample;

namespace Multithreading;

class Program
{
    static void Main(string[] args)
    {
        // Singlethread.Implement();
        // Multithread.Implement();
        // ThreadStartProgram.Implement();
        // Join.Implement();
        // Lock.Implement();
        // Lock.ImplementTryEnter();
        // PulseAndWait.Implement();
        // ManualResetEventClass.Implement();
        // AutoResetEventClass.Implement();
        // MutexClass.Implement();
        // SemaphoreClass.Implement();
        // DeadlockExample.DeadlockExample.Implement();
        // ThreadPoolClass.Implement();
        // TaskExample.Implement();

        // if we don't put await, the main thread will complete program execution while the child threads will still continue executing in the background
        // and we will only see execution started logs and no execution complete logs coz we won't be waiting for the task to complete
        // await will pass the thread back to thread pool for next task, while .Wait() will tell current thread to wait for the process to complete
        // AsyncAwait.Implement().Wait();
        
        // LockingExample.Implement();
        // SemaphoreSlimClass.Implement();
        // ConcurrentDictionaries.Implement();
        // ConcurrentQueues.Implement();
        // ConcurrentStacks.Implement();
        // ConcurrentBags.Implement();
        ConcurrentBagsExample.Implement();
    }
}
