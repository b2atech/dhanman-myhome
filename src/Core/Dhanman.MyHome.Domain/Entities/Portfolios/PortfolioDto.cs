using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dhanman.MyHome.Domain.Entities.Portfolios;

public sealed class PortfolioDto: EntityInt
{
    [Column("id")]
    public int Id { get; set; }

    [Column("apartment_id")]
    public Guid ApartmentId { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("description")]
    public string Description { get; set; } = default!;

    public PortfolioDto() { }

    public PortfolioDto(int id, Guid apartmentId, string name, string description)
    {
        Id = id;
        ApartmentId = apartmentId;
        Name = name;
        Description = description;
    }
}
