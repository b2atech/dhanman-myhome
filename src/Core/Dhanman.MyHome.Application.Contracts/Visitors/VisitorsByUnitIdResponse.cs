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
    #endregion

    #region Constructor 
    public VisitorsByUnitIdResponse(int visitorId, string visitorName, int unitId, string unitName, int? visitorTypeId, string visitorTypeName, int? serviceProviderSubTypeId, string serviceProviderSubTypeName)
    {        
        VisitorId = visitorId;
        VisitorName = visitorName;
        UnitId = unitId;
        UnitName = unitName;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
        ServiceProviderSubTypeId = serviceProviderSubTypeId;
        ServiceProviderSubTypeName = serviceProviderSubTypeName;
    }     
    #endregion
}