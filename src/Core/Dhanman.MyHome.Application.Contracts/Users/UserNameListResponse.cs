
namespace Dhanman.MyHome.Application.Contracts.Users;
public sealed class UserNameListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UserNameResponse> Items { get; }
    #endregion

    #region Constructor

    public UserNameListResponse(IReadOnlyCollection<UserNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}