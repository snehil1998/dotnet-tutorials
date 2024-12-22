namespace DesignPatterns.AbstractFactoryClass;

public interface IVehicleFactory
{
    public IVehicle Get(VehicleType type);
}