using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Users;

public sealed class LastName : ValueObject
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
    public static implicit operator string(LastName? lastName) => lastName?.Value ?? string.Empty;

    public static Result<LastName> Create(string? lastName) =>
       Result.Create(lastName, Errors.LastName.NullOrEmpty)
           .Ensure(l => !string.IsNullOrWhiteSpace(l), Errors.LastName.NullOrEmpty)
           .Ensure(l => l.Length <= MaxLength, Errors.LastName.LongerThanAllowed)
           .Map(l => new LastName(l));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    #endregion

}