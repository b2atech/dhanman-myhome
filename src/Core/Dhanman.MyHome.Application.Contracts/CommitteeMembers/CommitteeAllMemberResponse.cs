namespace Dhanman.MyHome.Application.Contracts.CommitteeMembers;

public sealed class CommitteeAllMemberResponse
{
    public CommitteeAllMemberResponse(int id, Guid userId, string memberName, DateTime start, DateTime end, int roleId, string roleName, int portfolioId, string portfolioName)
    {
        Id = id;
        UserId = userId;
        MemberName = memberName;
        EffectiveStartDate = start;
        EffectiveEndDate = end;
        RoleId = roleId;
        RoleName = roleName;
        PortfolioId = portfolioId;
        PortfolioName = portfolioName;
    }

    public int Id { get; }
    public Guid UserId { get; }
    public string MemberName { get; }
    public DateTime EffectiveStartDate { get; }
    public DateTime EffectiveEndDate { get; }
    public int RoleId { get; }
    public string RoleName { get; }
    public int PortfolioId { get; }
    public string PortfolioName { get; }
}