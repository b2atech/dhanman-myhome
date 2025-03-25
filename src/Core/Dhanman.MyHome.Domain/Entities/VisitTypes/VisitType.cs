using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitTypes;

public class VisitType : EntityInt
{
    #region Properties
    public string Name { get; set; }
    #endregion

    #region Constructor
    public VisitType(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
