using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public sealed class ValidateServiceProviderResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitDetailResponse> Items { get; }
    #endregion

    #region Constructor

    public ValidateServiceProviderResponse(IReadOnlyCollection<UnitDetailResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
