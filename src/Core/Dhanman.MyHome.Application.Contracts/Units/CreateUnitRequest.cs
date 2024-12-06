namespace Dhanman.MyHome.Application.Contracts.Units
{
    public class CreateUnitRequest
    {
        #region Properties
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int UnitTypeId { get; set; }
        public int OccupantId { get; set; }
        public int OccupancyId { get; set; }
        public decimal Area { get; set; }
        public decimal Bhk { get; set; }
        public int EIntercom { get; set; }
        public int PhoneExtension { get; set; }
        public Guid ApartmentId { get; set; }
        #endregion

        #region Constructor
        public CreateUnitRequest(string name,int buildingId, int floorId, int unitTypeId, int occupantId, int occupancyId, decimal area, decimal bhk, int eIntercom, int phoneExtension, Guid apartmentId)
        {
            Name = name;
            BuildingId = buildingId;
            FloorId = floorId;
            UnitTypeId = unitTypeId;
            OccupantId = occupantId;
            OccupancyId = occupancyId;
            Area = area;
            Bhk = bhk;
            EIntercom = eIntercom;
            PhoneExtension = phoneExtension;
            ApartmentId = apartmentId;
        }
        #endregion
    }

}