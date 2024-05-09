using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Domain.Entities.UnitTypes;
using System.Text.RegularExpressions;

namespace Dhanman.MyHome.Domain.Entities.Users;

public sealed class Email : ValueObject
{

    #region Properties

    public const int MaxLength = 256;

    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    public string Value { get; }
    #endregion

    #region Constructor
    private Email(string value) => Value = value;
    #endregion

    #region Methodes
    public static implicit operator string(Email? email) => email?.Value ?? string.Empty;

    public static explicit operator Email(string email) => Create(email).Value();

    public static Result<Email> Create(string? email) =>
     Result.Create(email, Errors.Email.NullOrEmpty)
         .Ensure(e => !string.IsNullOrWhiteSpace(e), Errors.Email.NullOrEmpty)
         .Ensure(e => e.Length <= MaxLength, Errors.Email.LongerThanAllowed)
         .Ensure(e => EmailFormatRegex.Value.IsMatch(e), Errors.Email.InvalidFormat)
         .Map(e => new Email(e));

    internal static Email Empty => new Email(string.Empty);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    #endregion

}