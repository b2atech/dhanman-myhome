namespace Dhanman.MyHome.Application.Contracts.Units
{
    public class CreateUnitsListRequest
    {
        #region Property
        public List<CreateUnitsRequest> UnitsList { get; set; }
        #endregion

        #region Constructor
        public CreateUnitsListRequest(List<CreateUnitsRequest> unitsList)
        {
            UnitsList = unitsList;
        }
        #endregion

    }
}
