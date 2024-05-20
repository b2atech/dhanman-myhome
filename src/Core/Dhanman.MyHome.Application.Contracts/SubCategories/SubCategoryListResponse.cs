namespace Dhanman.MyHome.Application.Contracts.SubCategories;

public sealed class SubCategoryListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<SubCategoryResponse> Items { get; }
    #endregion

    #region Constructor

    public SubCategoryListResponse(IReadOnlyCollection<SubCategoryResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
