
public static class LockingExample
{
    public static void Implement()
    {
        BookMyShow bookMyShow = new BookMyShow();
        
        // without locking, all three threads can access the method simultaneously and tickets will be available for all three threads
        Thread thread1 = new Thread(bookMyShow.Book)
        {
            Name = "Thread1"
        };
        Thread thread2 = new Thread(bookMyShow.Book)
        {
            Name = "Thread2"
        };
        Thread thread3 = new Thread(bookMyShow.Book)
        {
            Name = "Thread3"
        };

        // instead if we use locking, the threads will access the method synchronously and it should show tickets unavailable
        // Thread thread1 = new Thread(bookMyShow.BookLocked)
        // {
        //     Name = "Thread1"
        // };
        // Thread thread2 = new Thread(bookMyShow.BookLocked)
        // {
        //     Name = "Thread2"
        // };
        // Thread thread3 = new Thread(bookMyShow.BookLocked)
        // {
        //     Name = "Thread3"
        // };

        thread1.Start(1);
        thread2.Start(2);
        thread3.Start(3);
        Console.ReadKey();
    }
}