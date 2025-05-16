namespace Dhanman.MyHome.Application.Contracts.CommitteeMembers;

public class CommitteeMemberResponse
{
    public int Id { get; set; }
    public Guid ApartmentId { get; set; }
    public int RoleId { get; set; }
    public Guid UserId { get; set; }
    public string MemberName { get; set; }

    public CommitteeMemberResponse(int id,Guid apartmentId,int roleId,Guid userId, string memberName)
    {
        Id = id;
        ApartmentId = apartmentId;
        RoleId = roleId;
        UserId = userId;
        MemberName = memberName;
    }
}


