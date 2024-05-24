using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Contracts.Catergories;

public  sealed class CategoryListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<CategoryResponse> Items { get; }
    #endregion

    #region Constructor

    public CategoryListResponse(IReadOnlyCollection<CategoryResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
