using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Contracts.Users;

public sealed class UserListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UserResponse> Items { get; }
    #endregion

    #region Constructor

    public UserListResponse(IReadOnlyCollection<UserResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
