namespace DesignPatterns.AbstractFactoryClass;

public sealed class Car: IVehicle
{
    public Car(string model, int topSpeed)
    {
        _model = model;
        _topSpeed = topSpeed;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving {_model} at speed {_topSpeed}");
    }

    private readonly string  _model;
    private readonly int _topSpeed;
}