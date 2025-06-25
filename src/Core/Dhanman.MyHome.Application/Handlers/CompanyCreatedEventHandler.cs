using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Companies;
using Dhanman.Shared.Contracts.Events;
using Microsoft.Extensions.Logging;

namespace Dhanman.MyHome.Application.Handlers;

public class CompanyCreatedEventHandler : IMessageHandler<CreateCommonBasicCompanyEvent>
{
    #region Proerties

    private readonly ILogger<CompanyCreatedEventHandler> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor

    public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger, ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods

    public Task HandleAsync(CreateCommonBasicCompanyEvent companyCreatedEvent)
    {
        if (companyCreatedEvent == null)
        {
            _logger.LogWarning("Failed to deserialize CompanyCreatedEvent.");
            return Task.CompletedTask;
        }
        _logger.LogInformation("Processing CompanyCreatedEvent for CompanyId: {CompanyId}", companyCreatedEvent.BasicCompany.CompanyId);

        var company = new Company(
            companyCreatedEvent.BasicCompany.CompanyId,
            companyCreatedEvent.BasicCompany.OrganizationId,
            companyCreatedEvent.BasicCompany.Name,
            companyCreatedEvent.BasicCompany.IsApartment
        );

        _companyRepository.Insert(company);
        _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("CompanyCreatedEvent for CompanyId: {CompanyId} processed Successfully...!!!", companyCreatedEvent.BasicCompany.CompanyId);
        return Task.CompletedTask;
    }
    #endregion

}
