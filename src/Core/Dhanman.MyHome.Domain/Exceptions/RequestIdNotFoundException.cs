using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public class RequestIdNotFoundException : NotFoundException
{
    #region Constructor
    public RequestIdNotFoundException(int requestId)
        : base($"The request id with the identifier {requestId} was not found.")
    {

    }
    #endregion
}