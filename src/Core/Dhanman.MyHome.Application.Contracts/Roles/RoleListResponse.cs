namespace Dhanman.MyHome.Application.Contracts.Roles;

public sealed class RoleListResponse
{
    public RoleListResponse(IReadOnlyCollection<RoleResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }

    public IReadOnlyCollection<RoleResponse> Items { get; }
    public string Cursor { get; }
}