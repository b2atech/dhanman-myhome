namespace Dhanman.MyHome.Application.Contracts.OccupantTypes
{
    public sealed class OccupantTypeListResponse
    {
        #region Properties
        public string Cursor { get; }
        public IReadOnlyCollection<OccupantTypeResponse> Items { get; }

        #endregion

        #region Constructor
        public OccupantTypeListResponse(IReadOnlyCollection<OccupantTypeResponse> items, string cursor = "")
        {
            Items = items;
            Cursor = cursor;
        }
        #endregion
    }
}
