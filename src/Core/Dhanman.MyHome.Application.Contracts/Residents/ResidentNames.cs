using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Application.Contracts.Residents;

public class ResidentNames : EntityInt
{
    public int Id { get; set; }
    public string ResidentName { get; set; }
    public Guid UserId { get; set; }
}
