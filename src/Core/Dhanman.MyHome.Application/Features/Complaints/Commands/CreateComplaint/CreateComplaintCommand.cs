using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateComplaint;

public class CreateComplaintCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string DocLink { get; set; }
    public DateTime PrefferedTime { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int PriorityId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime OccuredDate { get; set; }
    public DateTime PrefferedDate { get; set; }
    public bool IsUrgent { get; set; }
    #endregion

    #region Constructor
    public CreateComplaintCommand() { }

    public CreateComplaintCommand(Guid id, string subject,string description,string docLink, DateTime prefferedTime, int categoryId, int subCategoryId, int priorityId, int departmentId, DateTime occuredDate, DateTime prefferedDate, bool isUrgent)
    {
        Id = id;
        Subject = subject;
        Description = description;
        DocLink = docLink;
        PrefferedTime = prefferedTime;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        PriorityId = priorityId;
        DepartmentId = departmentId;
        OccuredDate = occuredDate;
        PrefferedDate = prefferedDate;
        IsUrgent = isUrgent;
    }
    #endregion
}