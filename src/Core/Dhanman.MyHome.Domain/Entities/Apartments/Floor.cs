using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Apartments
{
    public class Floor : EntityInt, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public int TotalUnits { get; set; }
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
        public Floor(string name, int buildingId, int totalUnits, Guid createdBy)
        {
            Name = name;
            BuildingId = buildingId;
            TotalUnits = totalUnits;
            CreatedBy = createdBy;

        }
        #endregion
    }
}
