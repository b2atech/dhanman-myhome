using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dhanman.MyHome.Domain.Entities.CommitteeMembers;

    public class CommitteeMemberDto : EntityInt
    {
        public int Id { get; set; }

        [Column("apartment_id")]
        public Guid ApartmentId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("member_name")]
        public string MemberName { get; set; }

        public CommitteeMemberDto()
        {
        }

        public CommitteeMemberDto(int id, Guid apartmentId, int roleId, Guid userId, string memberName)
        {
            Id = id;
            ApartmentId = apartmentId;
            RoleId = roleId;
            UserId = userId;
            MemberName = memberName;
        }
}
