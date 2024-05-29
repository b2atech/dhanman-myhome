using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class FloorNotFoundException : NotFoundException
{
    public FloorNotFoundException(int floorId) : base($"The product with the identifier {floorId} was not found.")
    {

    }
}
