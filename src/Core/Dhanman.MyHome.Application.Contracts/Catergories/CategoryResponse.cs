namespace Dhanman.MyHome.Application.Contracts.Catergories;

public class CategoryResponse
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    #endregion

    #region Contructors
    public CategoryResponse(int id ,string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
