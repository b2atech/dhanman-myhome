using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Addresses;

public sealed class AddressLine : ValueObject
{
    public const int MaxLength = 500;

    private AddressLine(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(AddressLine? addressLine) => addressLine?.Value ?? string.Empty;

    public static Result<AddressLine> Create(string? addressLine) =>
        Result.Create(addressLine, Errors.AddressLine.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.AddressLine.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, Errors.AddressLine.LongerThanAllowed)
            .Map(f => new AddressLine(f));

    internal static AddressLine Empty => new AddressLine(string.Empty);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}