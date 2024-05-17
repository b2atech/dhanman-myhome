using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Complaints
{
    public class Complaint : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public string Subject { get; set; }
        public string Description { get; set; }
        public string DocLink { get; set; }
        public DateTime PrefferedTime { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int PriorityId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime OccuredDate { get; set; }
        public DateTime PrefferedDate { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime CreatedOnUtc { get; }
        public DateTime? ModifiedOnUtc { get; set; }
        public DateTime? DeletedOnUtc { get; }
        public bool IsDeleted { get; }
        public Guid CreatedBy { get; protected set; }
        public Guid? ModifiedBy { get; protected set; }
        #endregion

        #region Constructor
        public Complaint(Guid id, string subject, string description, string docLink,  DateTime prefferedTime, int categoryId, int subCategoryId, int priorityId, int departmentId, DateTime occuredDate, DateTime prefferedDate, bool isUrgent)
        {
            Id = id;
            Subject = subject;
            Description = description;
            DocLink = docLink;
            PrefferedTime = prefferedTime;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            PriorityId = priorityId;
            DepartmentId = departmentId;
            OccuredDate = occuredDate;
            PrefferedDate = prefferedDate;
            IsUrgent = isUrgent;
        }
        #endregion
    }
}