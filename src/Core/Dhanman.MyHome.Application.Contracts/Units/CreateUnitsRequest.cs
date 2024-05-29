namespace Dhanman.MyHome.Application.Contracts.Units
{
    public class CreateUnitsRequest
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
        #endregion

        #region Constructor
        public CreateUnitsRequest(string name,int buildingId, int floorId, int unitTypeId, int occupantId, int occupancyId, decimal area, decimal bhk, int eIntercom, int phoneExtension)
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
        }
        #endregion
    }

}
