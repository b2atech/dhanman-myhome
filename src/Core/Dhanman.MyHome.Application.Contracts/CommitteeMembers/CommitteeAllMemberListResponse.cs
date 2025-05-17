namespace Dhanman.MyHome.Application.Contracts.CommitteeMembers;


public sealed class CommitteeAllMemberListResponse
{
    public CommitteeAllMemberListResponse(IReadOnlyCollection<CommitteeAllMemberResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }

    public IReadOnlyCollection<CommitteeAllMemberResponse> Items { get; }
    public string Cursor { get; }
}