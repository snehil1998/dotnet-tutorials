namespace DesignPatterns.Builder;

public class CarBuilder
{
    private Car _car = new Car();

    public CarBuilder SetModel(string model)
    {
        _car.Model = model;
        return this;
    }

    public CarBuilder SetMake(string make)
    {
        _car.Make = make;
        return this;
    }

    public CarBuilder SetColor(string color)
    {
        _car.Color = color;
        return this;
    }

    public CarBuilder SetYear(int year)
    {
        _car.Year = year;
        return this;
    }

    public CarBuilder SetEngine(string engine)
    {
        _car.Engine = engine;
        return this;
    }
    public Car Build()
    {
        return _car;
    }
}