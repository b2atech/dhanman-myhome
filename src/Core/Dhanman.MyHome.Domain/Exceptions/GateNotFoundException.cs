using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class GateNotFoundException: NotFoundException
{
    public GateNotFoundException(int gateId) : base($"The gate with the identifier {gateId} was not found.")
    {

    }
}
