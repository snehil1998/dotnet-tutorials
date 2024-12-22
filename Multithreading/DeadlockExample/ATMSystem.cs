namespace Multithreading.DeadlockExample;

public sealed class ATMSystem
{
    public ATMSystem(ATMDetails FromATM, ATMDetails ToATM, double AmountToTransfer)
    {
        this.FromATM = FromATM;
        this.ToATM = ToATM;
        this.AmountToTransfer = AmountToTransfer;
    }

    public void Transfer()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock: {FromATM.ID}");
        lock(FromATM)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} processing request for 1 second");
            Thread.Sleep(1000);

            Console.WriteLine($"{Thread.CurrentThread.Name} finished processing and trying to lock: {ToATM.ID}");
            lock(ToATM)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} implementing logic for withdrawing and depositing amount");
                FromATM.Withdraw(AmountToTransfer);
                ToATM.Deposit(AmountToTransfer);
            }
        }
    }

    public void TransferReorderedLock()
    {
        // locks are reordered to resolve deadlock, so they lock dependencies in the same order hence running synchronously
        object _lock1, _lock2;
        if(FromATM.ID < ToATM.ID)
        {
            _lock1 = FromATM;
            _lock2 = ToATM;
        }
        else
        {
            _lock1 = ToATM;
            _lock2 = FromATM;
        }

        Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock: {((ATMDetails)_lock1).ID}");
        lock(_lock1)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} processing request for 1 second");
            Thread.Sleep(1000);

            Console.WriteLine($"{Thread.CurrentThread.Name} finished processing and trying to lock: {((ATMDetails)_lock2).ID}");
            lock(_lock2)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} implementing logic for withdrawing and depositing amount");
                FromATM.Withdraw(AmountToTransfer);
                ToATM.Deposit(AmountToTransfer);
                Console.WriteLine($"{Thread.CurrentThread.Name} finished implmenting logic");
            }
        }
    }

    public void TransferUsingMonitor()
    {
        // we check if try to acquire a lock times out after a few seconds and release the lock to avoid deadlock.
        // The logic is not implemented in this case but deadlock is avoided
        Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock: {FromATM.ID}");
        lock(FromATM)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} processing request for 1 second");
            Thread.Sleep(1000);

            Console.WriteLine($"{Thread.CurrentThread.Name} finished processing and trying to lock: {ToATM.ID}");
            if(Monitor.TryEnter(ToATM, 3000))
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} implementing logic for withdrawing and depositing amount");
                FromATM.Withdraw(AmountToTransfer);
                ToATM.Deposit(AmountToTransfer);
                Console.WriteLine($"{Thread.CurrentThread.Name} finished implmenting logic");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} failed to lock {ToATM.ID} and did not implement logic");
            }
        }
    }

    private ATMDetails FromATM { get; init; }
    private ATMDetails ToATM { get; init; }
    private double AmountToTransfer { get; init; }
}