using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Bus.ValueObjects;

public class PlateNumber : ValueObject
{
    public string Number { get; }
    
    public PlateNumber(string number)
    {
        Number = number;
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Number;
    }
}