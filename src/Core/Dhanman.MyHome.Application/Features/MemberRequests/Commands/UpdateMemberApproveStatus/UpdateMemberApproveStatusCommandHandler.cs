using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.MemberRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.UpdateMemberApproveStatus;

public class UpdateMemberApproveStatusCommandHandler : ICommandHandler<UpdateMemberApproveStatusCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IUnitRepository _unitRepository;    
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly ISalesServiceClient _salesServiceClient;    
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;
    #endregion

    #region Constructors
    public UpdateMemberApproveStatusCommandHandler(IUnitRepository unitRepository, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, 
                                                   IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository, 
                                                   IMediator mediator, IUserContextService userContextService)
    {
        _unitRepository = unitRepository;
        _commonServiceClient = commonServiceClient;
        _salesServiceClient = salesServiceClient;        
        _residentRequestRepository = residentRequestRepository;
        _residentRepository = residentRepository;
        _residentUnitRepository = residentUnitRepository;
        _mediator = mediator;
        _userContextService = userContextService;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMemberApproveStatusCommand request, CancellationToken cancellationToken)
    {
        var updateRequestApproveStatus = await _residentRequestRepository.GetBydIdIntAsync(request.Id);

        if (updateRequestApproveStatus is null)
        {
            throw new RequestIdNotFoundException(request.Id);
        }

        int lastId = await _unitRepository.GetLastUnitIdAsync();
        int newId = lastId + 1;

        var unitName = $"{updateRequestApproveStatus.FirstName ?? ""} {updateRequestApproveStatus.LastName ?? ""}".Trim();

        var unit = new Unit(newId, unitName, 117, 193, 1, 1, 1, 1, 1, 1, 1, "1.0", "1.1", updateRequestApproveStatus.ApartmentId, Guid.NewGuid());
        _unitRepository.Insert(unit);
        await _commonServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = unitName, CompanyId = updateRequestApproveStatus.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });
        await _salesServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = unitName, CompanyId = updateRequestApproveStatus.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });

        updateRequestApproveStatus.RequestStatusId = ResidentRequestStatus.APPROVED;
        updateRequestApproveStatus.UnitId = unit.Id;
        _residentRequestRepository.Update(updateRequestApproveStatus);

        int nextresidentId = _residentRepository.GetTotalRecordsCount() + 1;
        var resident = new Resident(nextresidentId, updateRequestApproveStatus.ApartmentId, updateRequestApproveStatus.FirstName, updateRequestApproveStatus.LastName, updateRequestApproveStatus.Email, updateRequestApproveStatus.ContactNumber, updateRequestApproveStatus.PermanentAddressId, updateRequestApproveStatus.ResidentTypeId, updateRequestApproveStatus.OccupancyStatusId);
        _residentRepository.Insert(resident);

        int nextresidentUnitId = _residentUnitRepository.GetTotalRecordsCount() + 1;
        var residentUnit = new ResidentUnit(nextresidentUnitId, unit.Id, nextresidentId);
        _residentUnitRepository.Insert(residentUnit);

        await _mediator.Publish(new MemberRequestUpdatedEvent(updateRequestApproveStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestApproveStatus.Id));
    }

    #endregion

}