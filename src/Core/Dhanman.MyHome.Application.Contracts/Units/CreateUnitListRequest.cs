namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class CreateUnitListRequest
{
	#region Property
	public List<UnitRequest> UnitList { get; set; }
    #endregion

    #region Constructor
    public CreateUnitListRequest(List<UnitRequest> unitList)
    {
        UnitList = unitList;
    }
    #endregion

}
