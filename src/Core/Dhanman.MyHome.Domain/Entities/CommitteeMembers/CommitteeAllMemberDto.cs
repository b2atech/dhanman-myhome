using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

public sealed class CommitteeAllMemberDto : EntityInt
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("member_name")]
    public string MemberName { get; set; }

    [Column("effective_start_date")]
    public DateTime EffectiveStartDate { get; set; }

    [Column("effective_end_date")]
    public DateTime EffectiveEndDate { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("role_name")]
    public string RoleName { get; set; } = default!;

    [Column("portfolio_id")]
    public int PortfolioId { get; set; }

    [Column("portfolio_name")]
    public string PortfolioName { get; set; } = default!;

    public CommitteeAllMemberDto() { }

    public CommitteeAllMemberDto(
        int id,
        Guid userId,
        string memberName,
        DateTime effectiveStartDate,
        DateTime effectiveEndDate,
        int roleId,
        string roleName,
        int portfolioId,
        string portfolioName)
    {
        Id = id;
        UserId = userId;
        MemberName = memberName;
        EffectiveStartDate = effectiveStartDate;
        EffectiveEndDate = effectiveEndDate;
        RoleId = roleId;
        RoleName = roleName;
        PortfolioId = portfolioId;
        PortfolioName = portfolioName;
    }
}