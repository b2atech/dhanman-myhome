namespace Dhanman.MyHome.Application.Contracts.SubCategories;

public class SubCategoryResponse
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int CatergoryId { get; set; }
    #endregion

    #region Contructors
    public SubCategoryResponse(int id, string name, int catergoryId)
    {
        Id = id;
        Name = name;
        CatergoryId = catergoryId;
    }
    #endregion
}
