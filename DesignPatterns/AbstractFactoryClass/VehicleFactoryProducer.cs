namespace DesignPatterns.AbstractFactoryClass;

public sealed class VehicleFactoryProducer
{
    public IVehicleFactory GetFactory(FactoryType factoryType)
    {
        if (factoryType == FactoryType.Sports)
        {
            return new SportsVehicleFactory();
        }
        else if (factoryType == FactoryType.Commercial)
        {
            return new CommercialVehicleFactory();
        }
        throw new Exception("Unknown factory type");
    }
}