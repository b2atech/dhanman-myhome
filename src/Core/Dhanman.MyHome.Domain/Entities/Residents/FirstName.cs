using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Residents;

public sealed class FirstName : ValueObject
{
    #region Properties
    public const int MaxLength = 100;

    public string Value { get; }

    internal static FirstName Empty => new FirstName(string.Empty);
    #endregion

    #region Constructors
    private FirstName(string value) => Value = value;
    #endregion

    #region Methodes
    public static implicit operator string(FirstName? name) => name?.Value ?? string.Empty;

    public static Result<FirstName> Create(string? name) =>
       Result.Create(name, Errors.Name.NullOrEmpty)
           .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.Name.NullOrEmpty)
           .Ensure(f => f.Length <= MaxLength, Errors.Name.LongerThanAllowed)
           .Map(f => new FirstName(f));
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    #endregion
}
