namespace Dhanman.MyHome.Application.Contracts.OccupantTypes
{
    public sealed class OccupantTypeResponse
    {
        #region Properties 
        public int Id { get; }
        public string Name { get; }

        #endregion

        #region Constructor
        public OccupantTypeResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
