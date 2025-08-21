namespace Dhanman.MyHome.Application.Contracts.Units;

public class BasicUnitInfoResponse
{
    #region Properties 

    public BasicUnitInfo Item { get; }
    public string Cursor { get; }

    #endregion

    #region Constructor

    public BasicUnitInfoResponse(BasicUnitInfo items, string cursor = "")
    {
        Item = items;
        Cursor = cursor;
    }
    #endregion
}
