using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Portfolios;

public class Portfolio : EntityInt
{
    public Guid ApartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsDeleted { get; }

    public Portfolio() { }

    public Portfolio(int id, Guid apartmentId, string name, string? description)
    {
        Id = id;
        ApartmentId = apartmentId;
        Name = name;
        Description = description;
    }
}

