namespace DesignPatterns.Singleton;

public sealed class SingletonClass
{
    private static SingletonClass? Instance = null;
    private SingletonClass() {}

    public static SingletonClass GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SingletonClass();
        }
        return Instance;
    }

    public void GetText()
    {
        Console.WriteLine("This is a SingletonClass instance");
    }
}