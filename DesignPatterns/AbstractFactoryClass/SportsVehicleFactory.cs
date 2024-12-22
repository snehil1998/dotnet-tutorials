namespace DesignPatterns.AbstractFactoryClass;

public sealed class SportsVehicleFactory: IVehicleFactory
{
    public IVehicle Get(VehicleType type)
    {
        if (type == VehicleType.Car) return new Car("BMW", 250);
        if(type == VehicleType.Bike) return new Bike("Honda", 200);
        throw new Exception("Vehicle type not found");
    }

    // we can also create separate methods for car and bike instead of type checking if we want to pass different parameter types/ number of parameters
    // public IVehicle GetCar(string model, int topSpeed, int wheels)
    // {
    //     return new Car(model, topSpeed, wheels);
    // }
    // public IVehicle GetBike(string model, int topSpeed)
    // {
    //     return new Bike(model, topSpeed);
    // }
}