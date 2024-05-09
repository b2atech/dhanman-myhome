using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.CommonEntities;

public sealed class Name : ValueObject
{
    #region Properties
    public const int MaxLength = 100;

    public string Value { get; }

    internal static Name Empty => new Name(string.Empty);
    #endregion

    #region Constructors
    private Name(string value) => Value = value;
    #endregion

    #region Methodes
    public static implicit operator string(Name? name) => name?.Value ?? string.Empty;

    public static Result<Name> Create(string? name) =>
       Result.Create(name, Errors.Name.NullOrEmpty)
           .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.Name.NullOrEmpty)
           .Ensure(f => f.Length <= MaxLength, Errors.Name.LongerThanAllowed)
           .Map(f => new Name(f));
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    #endregion
}
