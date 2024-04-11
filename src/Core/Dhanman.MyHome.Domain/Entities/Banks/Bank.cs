using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanan.MyHome.Domain.Entities.Banks;
 public class Bank : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string AccountNumber { get; set; }
    public string IFSC { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructors
    public Bank() { }
    public Bank(Guid id, string bankName, string branchName, string accountNumber, string iFSC)
    {
        Id = id;
        BankName = bankName;
        BranchName = branchName;
        AccountNumber = accountNumber;
        IFSC = iFSC;
    }
    #endregion
}