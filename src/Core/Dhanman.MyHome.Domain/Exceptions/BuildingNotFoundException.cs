using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class BuildingNotFoundException : NotFoundException
{
    public BuildingNotFoundException(int buildingId) : base($"The product with the identifier {buildingId} was not found.")
    {

    }
}
