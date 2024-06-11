namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public class AssignUnitListResponse
{

    public IReadOnlyCollection<ServiceProviderAssignedUnitResponse> Items { get; set; }
    public string Cursor { get; set; }

    public AssignUnitListResponse(IReadOnlyCollection<ServiceProviderAssignedUnitResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    
}
