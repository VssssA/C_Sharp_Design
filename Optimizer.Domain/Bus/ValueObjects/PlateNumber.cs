using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Bus.ValueObjects;

public sealed class PlateNumber : ValueObject
{
    public string Number { get; }
    
    private PlateNumber(string number)
    {
        Number = number;
    }

    public static PlateNumber Create(string number)
    {
        return new PlateNumber(number);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Number;
    }
}