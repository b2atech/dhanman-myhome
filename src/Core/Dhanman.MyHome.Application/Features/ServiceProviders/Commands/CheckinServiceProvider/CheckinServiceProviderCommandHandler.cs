using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ServiceProviderLogs;
using Dhanman.MyHome.Domain;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CheckinServiceProvider;

public class CheckinServiceProviderCommandHandler : ICommandHandler<CheckinServiceProviderCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IServiceProviderRepository _serviceProviderRepository;
    private readonly IServiceProviderLogRepository _serviceProviderLogRepository;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor
    public CheckinServiceProviderCommandHandler(
        IServiceProviderRepository serviceProviderRepository,
        IServiceProviderLogRepository serviceProviderLogRepository,
        IUnitOfWork unitOfWork)
    {
        _serviceProviderRepository = serviceProviderRepository;
        _serviceProviderLogRepository = serviceProviderLogRepository;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CheckinServiceProviderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Step 1: Validate service provider by apartmentId and pin
            var serviceProvider = await _serviceProviderRepository.GetByApartmentIdAndPinAsync(request.ApartmentId, request.Pin);
            
            if (serviceProvider == null)
            {
                return Result.Failure<EntityCreatedResponse>(Errors.ServiceProvider.NotFound);
            }

            // Step 2: Create service provider log entry
            var serviceProviderLog = new ServiceProviderLog(
                serviceProviderId: serviceProvider.Id,
                visitingUnitId: 1, // Placeholder value as per requirements
                visitPurposeId: 1, // Placeholder value as per requirements
                visitingFrom: serviceProvider.VisitingFrom,
                currentStatusId: 1, // Placeholder value as per requirements
                entryTime: DateTime.UtcNow,
                exitTime: null // Placeholder value as per requirements
            );

            _serviceProviderLogRepository.Insert(serviceProviderLog);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new EntityCreatedResponse(serviceProviderLog.Id));
        }
        catch (Exception)
        {
            return Result.Failure<EntityCreatedResponse>(Errors.General.ServerError);
        }
    }
    #endregion
}