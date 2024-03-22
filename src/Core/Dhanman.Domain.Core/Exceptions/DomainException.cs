using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.Domain.Core.Exceptions;

public class DomainException : Exception
{
    public DomainException(Error error) => Error = error;

    public Error Error { get; }
}