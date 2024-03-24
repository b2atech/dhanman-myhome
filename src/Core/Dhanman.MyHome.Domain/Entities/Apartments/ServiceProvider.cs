using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class ServiceProvider : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string VisitingFrom { get; set; }
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }
        //Daily Help
        //Tutors
        //Handyman
        //Vendors
        //Medical Help
        //Transport
        //Society Security
        //Society Administration Staff
        //Society Maintenance Staff
        //Full Time Helps
        public int ServiceProviderTypeId { get; set; }
        public string? VehicleNumber { get; set; }
        public int IdentityTypeId { get; set; }
        public string IdentityId { get; set; }
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
        public ServiceProvider(string firstName, string? lastName, string? email, string visitingFrom, string contactNumber, int addressId, int serviceProviderTypeId, string? vehicleNumber, int identityTypeId, string identityId, Guid createdBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            VisitingFrom = visitingFrom;
            ContactNumber = contactNumber;
            AddressId = addressId;
            ServiceProviderTypeId = serviceProviderTypeId;
            VehicleNumber = vehicleNumber;
            IdentityTypeId = identityTypeId;
            IdentityId = identityId;
            CreatedBy = createdBy;
        }
        #endregion
    }
}
