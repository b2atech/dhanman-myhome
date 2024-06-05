using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public class VisitorNotFoundException : NotFoundException
{
    public VisitorNotFoundException(int visitorId) : base($"The visitor with the identifier {visitorId} was not found.")
    {

    }
}

