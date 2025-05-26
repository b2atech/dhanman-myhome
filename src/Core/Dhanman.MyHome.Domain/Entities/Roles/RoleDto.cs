using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dhanman.MyHome.Domain.Entities.Roles;

public sealed class RoleDto: EntityInt
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("description")]
    public string Description { get; set; } = default!;

    public RoleDto() { }

    public RoleDto(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}