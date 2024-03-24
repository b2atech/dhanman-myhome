using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class Gate : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public string Name { get; set; }
        public Guid ApartmentId { get; set; }
        public int? BuildingId { get; set; }
        public int GateTypeId { get; set; }
        public bool IsUsedForIn { get; set; }
        public bool IsUsedForOut { get; set; }
        public bool IsAllUsersAllowed{ get; set; }
        public bool IsResidentsAllowed { get; set; }
        public bool IsStaffAllowed { get; set; }
        public bool IsVendorAllowed { get; set; }
       
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
        public Gate(string name,Guid apartmentId, int? buildingId, int gateTypeId,
            bool isUsedForIn,
            bool isUsedForOut,
            bool isAllUsersAllowed,
            bool isResidentsAllowed,
            bool isStaffAllowed,
            bool isVendorAllowed,
            Guid createdBy)
        {
            Name = name;
            ApartmentId = apartmentId;
            BuildingId = buildingId;
            GateTypeId = gateTypeId;
            IsUsedForIn = isUsedForIn;
            IsUsedForOut = isUsedForOut;
            IsAllUsersAllowed = isAllUsersAllowed;
            IsResidentsAllowed = isResidentsAllowed;
            IsStaffAllowed = isStaffAllowed;
            IsVendorAllowed = isVendorAllowed;
            CreatedBy = createdBy;
        }
        #endregion
    }
}
