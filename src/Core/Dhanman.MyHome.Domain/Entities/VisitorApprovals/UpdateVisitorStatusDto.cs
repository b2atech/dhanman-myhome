using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorApprovals;

public sealed class UpdateVisitorStatusDto: EntityInt
{


    public int VisitorId { get; set; }
    public int TotalVisitsCount { get; set; }
    public int ApprovedVisitsCount { get; set; }
    public int FinalStatusId { get; set; }

    public UpdateVisitorStatusDto() { }

    public UpdateVisitorStatusDto(int visitorId, int totalVisitorCount, int approvedVisitsCount, int finalStatusId)
    {
        VisitorId = visitorId;
        TotalVisitsCount = totalVisitorCount;
        ApprovedVisitsCount = approvedVisitsCount;
        FinalStatusId = finalStatusId;
    }
}