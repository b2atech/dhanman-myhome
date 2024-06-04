namespace Dhanman.MyHome.Application.Contracts.Buildings
{
    public sealed class UpdateBuildingRequest
    {
        #region Properties
        public int Id { get; set; }
        public Guid ApartmentId { get; set; }
        public string Name { get; set; }
        public int BuildingTypeId { get; set; }
        public int TotalUnits { get; set; }
        #endregion

        public UpdateBuildingRequest() => Name = string.Empty;
    }
}
