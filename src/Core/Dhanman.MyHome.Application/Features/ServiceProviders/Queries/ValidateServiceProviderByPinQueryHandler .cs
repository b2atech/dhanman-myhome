using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Queries;

public class ValidateServiceProviderByPinQueryHandler : IQueryHandler<ValidateServiceProviderByPinQuery, Result<ServiceProviderValidationResponse>>
{
    private readonly IServiceProviderRepository _serviceProviderRepository;
    public ValidateServiceProviderByPinQueryHandler(IServiceProviderRepository serviceProviderRepository)
    {
        _serviceProviderRepository = serviceProviderRepository;
    }

    public async Task<Result<ServiceProviderValidationResponse>> Handle(ValidateServiceProviderByPinQuery request, CancellationToken cancellationToken)
    {
        var serviceProviderExists = await _serviceProviderRepository
            .GetByPinAsync(request.Pin);

        var response = new ServiceProviderValidationResponse
        {
            PinCode = request.Pin,
            IsValid = serviceProviderExists,
            Message = serviceProviderExists ? "Service provider found." : "Service provider not found."
        };

        return Result.Success(response);
    }
}