namespace Dhanman.MyHome.Application.Contracts.Roles;

public sealed class RoleResponse
{
    public RoleResponse(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
}

