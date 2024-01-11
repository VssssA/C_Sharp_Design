using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;

namespace Optimizer.Domain.Bus;

public sealed class Bus : Transport<BusId>
{
    public PlateNumber PlateNumber { get; private set; }

    private Bus(
        BusId id,
        int maxPassengersCount,
        PlateNumber plateNumber) : base(id, maxPassengersCount)
    {
        PlateNumber = plateNumber;
    }

    public static Bus Create(
        int maxPassengersCount,
        PlateNumber plateNumber)
    {
        return new Bus(BusId.CreateUnique(), maxPassengersCount, plateNumber);
    }

    public override string ToString() =>
        $"PlateNumber: {PlateNumber}, MaxPassengersCount: {MaxPassengersCount}";
}