namespace Dhanman.MyHome.Application.Contracts.Visitors;
 
public sealed class VisitorsByUnitIdResponse
{
    #region Properties   
    public int VisitorId { get; set; }
    public string VisitorName { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitorTypeName { get; set; }
    public int? ServiceProviderSubTypeId { get; set; }
    public string ServiceProviderSubTypeName { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    #endregion

    #region Constructor 
    public VisitorsByUnitIdResponse(int visitorId, string visitorName, int unitId, string unitName, int? visitorTypeId, string visitorTypeName, int? serviceProviderSubTypeId, string serviceProviderSubTypeName, DateTime entryTime, DateTime? exitTime)
    {        
        VisitorId = visitorId;
        VisitorName = visitorName;
        UnitId = unitId;
        UnitName = unitName;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
        ServiceProviderSubTypeId = serviceProviderSubTypeId;
        ServiceProviderSubTypeName = serviceProviderSubTypeName;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }     
    #endregion
}