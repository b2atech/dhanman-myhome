namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public  class ApprovalActionResponse
{
    public bool Success { get; set; }
    public ApprovalActionResponse() { }
    public ApprovalActionResponse(bool success)
    {
        Success = success;
    }
}
