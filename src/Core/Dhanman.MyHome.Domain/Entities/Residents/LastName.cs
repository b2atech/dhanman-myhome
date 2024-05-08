using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Residents;

internal class LastName: ValueObject
{
    #region Properties
    public const int MaxLength = 100;

    public string Value { get; }

    internal static LastName Empty => new LastName(string.Empty);
    #endregion

    #region Constructors
    private LastName(string value) => Value = value;
    #endregion

    #region Methodes
    public static implicit operator string(LastName? name) => name?.Value ?? string.Empty;

    public static Result<LastName> Create(string? name) =>
       Result.Create(name, Errors.LastName.NullOrEmpty)
           .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.LastName.NullOrEmpty)
           .Ensure(f => f.Length <= MaxLength, Errors.LastName.LongerThanAllowed)
           .Map(f => new LastName(f));
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    #endregion
}
