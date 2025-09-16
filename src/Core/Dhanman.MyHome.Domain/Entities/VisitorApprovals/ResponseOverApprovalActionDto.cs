using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorApprovals;

public sealed class ResponseOverApprovalActionDto: EntityInt
{


    public bool Success { get; set; }

    public ResponseOverApprovalActionDto() { }

    public ResponseOverApprovalActionDto(bool success)
    {
        Success = success;
    }
}