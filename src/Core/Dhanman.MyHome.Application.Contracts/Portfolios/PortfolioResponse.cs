namespace Dhanman.MyHome.Application.Contracts.Portfolios;

public sealed class PortfolioResponse
{
    public PortfolioResponse(int id, Guid apartmentId, string name, string description)
    {
        Id = id;
        ApartmentId = apartmentId;
        Name = name;
        Description = description;
    }

    public int Id { get; }
    public Guid ApartmentId { get; }
    public string Name { get; }
    public string Description { get; }
}

