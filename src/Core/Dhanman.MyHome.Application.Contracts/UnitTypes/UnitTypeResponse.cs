namespace Dhanman.MyHome.Application.Contracts.UnitTypes
{
    public sealed class UnitTypeResponse
    {
        #region Properties 
        public int Id { get; }
        public string Name { get; }

        #endregion

        #region Constructor
        public UnitTypeResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
