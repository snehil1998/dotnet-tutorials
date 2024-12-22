namespace Multithreading;

public static class ThreadStartProgram {
    public static void Implement() {
        //it is a delegate which mean the method signature should match the delegate signature
        // ThreadStart threadStart = new ThreadStart(Method1);
        // ThreadStart threadStart = Method1;
        // ThreadStart threadStart = delegate() { Method1(); };
        // ThreadStart threadStart = () => Method();

        //parameterized threadstart. Also provide value for parameterized in t.Start(). 
        //It won't give an error if we pass a different type (string bool, etc.) coz not type safe,
        //but will give runtime error
        // ParameterizedThreadStart threadStart = new ParameterizedThreadStart(ParameterisedMethod);

        // How to make it type safe? By creating a helper class for the method where we pass the type
        int number = 10;
        NumberHelper numberHelper = new NumberHelper(number);
        ThreadStart threadStart = new ThreadStart(numberHelper.Method);
        
        Thread t = new Thread(threadStart);
        t.Start();     
        Console.ReadLine();
    }

    public static void Method() {
        for(var i=0; i< 5; i++) {
            Console.WriteLine($"Method: {i}");
        }
    }

    public static void ParameterisedMethod(object? number) {
        for(var i=0; i< Convert.ToInt32(number); i++) {
            Console.WriteLine($"Param Method: {i}");
        }
    }
}

public sealed class NumberHelper {
    private int Number;

    public NumberHelper(int number) {
        Number = number;
    }

    public void Method()
    {
        for(int i=0; i<Number; i++) {
            Console.WriteLine($"Param Method: {i}");
        }
    }
}