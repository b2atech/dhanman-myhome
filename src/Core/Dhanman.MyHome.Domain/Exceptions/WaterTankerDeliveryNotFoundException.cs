using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public sealed class WaterTankerDeliveryNotFoundException : NotFoundException
{
    #region Constructor
    public WaterTankerDeliveryNotFoundException(int waterTankerDeliveryId)
        : base($"The water tanker delivery with the identifier {waterTankerDeliveryId} was not found.")
    {

    }
    #endregion
}