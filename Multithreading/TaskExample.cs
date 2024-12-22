public class TaskExample {
    public static void Implement()
    {
        Console.WriteLine("Main thread has started");

        // Task task = new Task(SomeMethod); //ex1. instantiate a task and start it separately
        // task.Start();

        // Task task = Task.Factory.StartNew(SomeMethod); //ex2. or you can use the task factory property, instead of creating Task object and Start()

        // Task task = Task.Run(() => SomeMethod()); //ex3. recommended method to create task object

        // Task<int> task = Task.Run(() => {   //ex4. task with a return type method
        //     return SomeMethod(5);
        // });
        // Console.WriteLine("Output result: " + task.Result);

        // Task<Student> task = Task.Run(() => {   //ex5. returns a complex type value
        //     return new Student(1, "Snehil Kumar");
        // });
        // Console.WriteLine($"Print complex datatype: {task.Result.ID}, {task.Result.Name}");

        //ex6. task with continueWith to chain tasks. Here the task completes execution after the final ContinueWith executes
        Task<string> task = Task.Run(() => {
            return SomeMethod(5);
        }).ContinueWith(x => {
            return x.Result < 7;
        }).ContinueWith(boolean =>
        {
            if (boolean.Result) {
                return "The sum is less than 7";
            }
            return "The Sum is greater than or equal to 7";
            // throw new Exception("The Sum is greater than or equal to 7"); // to test exception message in continuationWith
        });
        task.ContinueWith(x => {
            var str = $"Another ContinuationWith after task execution: {x.IsFaulted}, {x.Exception?.Message}, {x.IsCompleted}"; //some other methods
            Console.WriteLine(str); // since task is already executed/completed, main thread will not wait on task.Wait()
        });
        task.ContinueWith(x => {    // runs if task is faulted/exception thrown
            Console.WriteLine("Task faulted: "+ x.Exception?.Message);
        }, TaskContinuationOptions.OnlyOnFaulted);
        task.ContinueWith(x => { // runs if task is cancelled
            Console.WriteLine("Task cancelled");
        }, TaskContinuationOptions.OnlyOnCanceled);
        task.ContinueWith(x => {    // runs if task is completed
            Console.WriteLine("Task completed: " + x.Result);
        }, TaskContinuationOptions.OnlyOnRanToCompletion);

        task.Wait(); // main thread waits for task to complete execution

        Console.WriteLine("Main thread has ended");
        Console.ReadLine();
    }

    private static void SomeMethod()
    {
        Console.WriteLine("Child thread has started: " + Thread.CurrentThread.ManagedThreadId);
        for(int i=0; i<5; i++)
        {
            Console.WriteLine("Loop " + i);
        }
        Console.WriteLine("Child thread has ended: " + Thread.CurrentThread.ManagedThreadId);
    }

    private static int SomeMethod(int num)
    {
        Console.WriteLine("Child thread has started: " + Thread.CurrentThread.ManagedThreadId);
        int Sum = 0;
        for(int i=0; i<num; i++)
        {
            Sum += i;
        }
        Console.WriteLine("Child thread has ended: " + Thread.CurrentThread.ManagedThreadId);
        return Sum;
    }
}

public record Student(int ID, string Name);