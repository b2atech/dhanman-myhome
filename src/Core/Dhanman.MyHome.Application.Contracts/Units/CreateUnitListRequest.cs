namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class CreateUnitListRequest
{
	#region Property
	public List<CreateUnitRequest> UnitList { get; set; }
    #endregion

    #region Constructor
    public CreateUnitListRequest(List<CreateUnitRequest> unitList)
    {
        UnitList = unitList;
    }
    #endregion


}
