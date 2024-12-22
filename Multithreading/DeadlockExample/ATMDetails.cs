namespace Multithreading.DeadlockExample;

public sealed class ATMDetails 
{
    public ATMDetails(int ID, double Balance)
    {
        this.ID = ID;
        this.Balance = Balance;
    }

    public void Withdraw(double AmountToTransfer)
    {
        Balance -= AmountToTransfer;
    }

    public void Deposit(double AmountToTransfer)
    {
        Balance += AmountToTransfer;
    }

    public readonly int ID;
    private double Balance;
}