using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class Resident : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public int UnitId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string? PermanentAddressId { get; set; }

        //Co-Owner, Multi Tenant, Owner, Owner Family , Tenant, Tenant Family 
        public string ResidentTypeId { get; set; }

        //Vacant , Residing , Let out to multiple tenants , Let out to one tenant, 
        public string OccupancyStatusId { get; set; }
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
        public Resident(int unitId, string firstName, string lastName, string email, string contactNumber, string? permanentAddressId, string residentTypeId, string occupancyStatusId, Guid createdBy)
        {
            UnitId = unitId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNumber = contactNumber;
            PermanentAddressId = permanentAddressId;
            ResidentTypeId = residentTypeId;
            OccupancyStatusId = occupancyStatusId;
            CreatedBy = createdBy;
        }
        #endregion
    }
}
