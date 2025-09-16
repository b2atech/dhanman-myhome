namespace Dhanman.MyHome.Application.Contracts.Visitors
{
    public class VisitorByContactListResponse
    {

        #region Properties 
        public string Cursor { get; }
        public IReadOnlyCollection<VisitorByContactResponse> Items { get; }
        #endregion

        #region Constructor

        public VisitorByContactListResponse(IReadOnlyCollection<VisitorByContactResponse> items, string cursor = "")
        {
            Items = items;
            Cursor = cursor;
        }
        #endregion
    }
}
