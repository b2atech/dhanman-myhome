using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class Apartment : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public string Name { get; set; }
        public int ApartmentTypeId { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string AssociationName { get; set; }
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
        public Apartment(string name, int apartmentTypeId,
            int addressId, string phone, string pan, string tan, string associationName,
               Guid createdBy)
        {
            Name = name;
            ApartmentTypeId = apartmentTypeId;
            AddressId = addressId;
            Phone = phone;
            PAN = pan;
            TAN = tan;
            AssociationName = associationName;
            CreatedBy = createdBy;

        }
        #endregion
    }
}
