namespace Multithreading.DeadlockExample;

public class DeadlockExample
{
    public static void Implement()
    {
        ATMDetails details1 = new(1, 1000);
        ATMDetails details2 = new(2, 2000);

        ATMSystem atmSystem1 = new(details1, details2, 10); // should lock details1 first and then try to lock details2
        ATMSystem atmSystem2 = new(details2, details1, 20); // should lock details2 first and then try to lock details1

        Thread thread1 = new Thread(atmSystem1.TransferUsingMonitor) { Name = "Thread1" };
        Thread thread2 = new Thread(atmSystem2.TransferUsingMonitor) { Name = "Thread2" };

        thread1.Start();
        thread2.Start();

        // should lead to a deadlock coz thread1 chas already locked details1 and thread2 has locked details2 for withdraw. 
        // Now after 1 sec, when they try to lock details2 and details1 respectively for deposit, it will lead to a deadlock

        thread1.Join();
        thread2.Join();
    }
}