using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class VisitorLog : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public int VisitorId { get; set; }
        public int VisitingUnitId { get; set; }
        public int? VisitPurposeId { get; set; }
        public string VisitingFrom { get; set; }
        //checked-in, checked-out, pending approval
        public int CurrentStatusId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        #endregion

        #region Audit Properties
        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool IsDeleted { get; }

        public Guid CreatedBy { get; protected set; }

        public Guid? ModifiedBy { get; protected set; }
        #endregion

        #region Constructor
        public VisitorLog(int visitorId, int visitingUnitId, int? visitPurposeId,string visitingFrom, int currentStatusId, DateTime entryTime, DateTime? exitTime, Guid createdBy)
        {
            VisitorId = visitorId;
            VisitingUnitId = visitingUnitId;
            VisitPurposeId = visitPurposeId;
            VisitingFrom = visitingFrom;
            CurrentStatusId = currentStatusId;
            EntryTime = entryTime;
            ExitTime = exitTime;
            CreatedBy = createdBy;
        }
        #endregion
    }
}
