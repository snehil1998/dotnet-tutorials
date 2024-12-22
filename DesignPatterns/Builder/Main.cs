namespace DesignPatterns.Builder;

public static class BuilderMain
{
    public static void Implement()
    {
        CarBuilder _carBuilder = new CarBuilder();
        Car result = _carBuilder
                        .SetModel("Camry")
                        .SetMake("Toyota")
                        .SetYear(2024)
                        .SetEngine("V8")
                        .SetColor("Red")
                        .Build();
        Console.WriteLine(result);            
    }
}