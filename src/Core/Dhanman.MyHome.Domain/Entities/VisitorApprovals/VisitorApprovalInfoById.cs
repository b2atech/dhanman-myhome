using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorApprovals;

public class VisitorApprovalInfoById : EntityInt
{
    #region Properties
    public int VisitorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly EntryTime { get; set; }
    public TimeOnly ExitTime { get; set; }
    public string CreatedByFirstName { get; set; }
    public string CreatedByLastName { get; set; }
    public string UnitName { get; set; }
    public string ApartmentName { get; set; }
    public string CityName { get; set; }

    #endregion
}
