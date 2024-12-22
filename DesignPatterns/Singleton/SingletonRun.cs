namespace DesignPatterns.Singleton;

public static class SingletonRun {
    public static void Implement()
    {
        SingletonClass singletonClass = SingletonClass.GetInstance();
        singletonClass.GetText();
    }
}