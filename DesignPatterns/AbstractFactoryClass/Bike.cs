namespace DesignPatterns.AbstractFactoryClass;

public sealed class Bike: IVehicle
{
    public Bike(string model, int topSpeed)
    {
        _model = model;
        _topSpeed = topSpeed;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving {_model} at top speed {_topSpeed}");
    }

    private readonly string  _model;
    private readonly int _topSpeed;
}