using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ApprovedVisitors;

public class ApprovedVisitorInfoById : EntityInt
{
    #region Properties
    public int Visitor_Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get;  set;}
    public string Contact_Number { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set;}
    public TimeOnly EntryTime { get; set; }
    public TimeOnly ExitTime { get; set; }
    public string CreatedByFirstName { get; set; }
    public string CreatedByLastName { get; set; }
    public string UnitName { get; set; }
    public string ApartmentName { get; set; }
    public string CityName { get; set; }

    #endregion
}
