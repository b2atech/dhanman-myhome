using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;

namespace Dhanman.MyHome.Domain.Entities.Users;

public sealed class ContactNumber: ValueObject
{
    #region Properties
    public const int MaxLength = 15;

    public string Value { get; }

    internal static ContactNumber Empty => new ContactNumber(string.Empty);
    #endregion

    #region Constructors

    public ContactNumber(string value) => Value = value;
    #endregion

    #region Methodes
    public static implicit operator string(ContactNumber? contactNumber) => contactNumber?.Value ?? string.Empty;

    public static Result<ContactNumber> Create(string? contactNumber) =>
       Result.Create(contactNumber, Errors.PhoneNumber.NullOrEmpty)
           .Ensure(f => !string.IsNullOrWhiteSpace(f), Errors.PhoneNumber.NullOrEmpty)
           .Ensure(f => f.Length <= MaxLength, Errors.PhoneNumber.LongerThanAllowed)
           .Map(f => new ContactNumber(f));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    #endregion
}
