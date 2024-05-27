namespace Dhanman.MyHome.Application.Contracts.UnitTypes
{
    public sealed class UnitTypeListResponse
    {
        #region Properties
        public string Cursor { get; }
        public IReadOnlyCollection<UnitTypeResponse> Items { get; }

        #endregion

        #region Constructor
        public UnitTypeListResponse(IReadOnlyCollection<UnitTypeResponse> items, string cursor = "")
        {
            Items = items;
            Cursor = cursor;
        }
        #endregion
    }
}
