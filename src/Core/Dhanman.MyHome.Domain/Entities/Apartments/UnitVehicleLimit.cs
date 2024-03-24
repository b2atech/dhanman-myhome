using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class UnitVehicleLimit : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public int UnitId { get; set; }
        public int CarLimit { get; set; }
        public int TwoWheelarsLimit { get; set; }
        public int NoOfCarsAllotted { get; set; }
        public int NoOfTwoWheelarsAllotted { get; set; }
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
        public UnitVehicleLimit(int unitId, int carLimit, int twoWheelarsLimit, int noOfCarsAllotted, int noOfTwoWheelarsAllotted, Guid createdBy)
        {
            UnitId = unitId;
            CarLimit = carLimit;
            TwoWheelarsLimit = twoWheelarsLimit;
            NoOfCarsAllotted = noOfCarsAllotted;
            NoOfTwoWheelarsAllotted = noOfTwoWheelarsAllotted;
            CreatedBy = createdBy;

        }
        #endregion
    }
}
