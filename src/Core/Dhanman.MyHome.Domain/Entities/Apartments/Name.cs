using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Name : ValueObject
{
    public const int MaxLength = 50;

    private Name(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Name? name) => name?.Value ?? string.Empty;

    public static Result<Name> Create(string? name) =>
        Result.Create(name, Errors.Name.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.Name.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, Errors.Name.LongerThanAllowed)
            .Map(f => new Name(f));

    internal static Name Empty => new Name(string.Empty);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}
