using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Features.Companies.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Companies;
using Dhanman.Shared.Contracts.Commands;
using Dhanman.Shared.Contracts.Common;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Companies.Commands;

public class CreateCompanyCommandHandler : ICommandHandler<CreateBasicCompanyCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly ICompanyRepository _companyRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor
    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateBasicCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company(request.CompanyId, request.OrganizationId,request.Name, request.IsApartment);

        _companyRepository.Insert(company);

        await _unitOfWork.SaveChangesAsync(request.MessageContext.UserId ,cancellationToken);

        await _mediator.Publish(new CompanyCreatedEvent(company.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(company.Id));
    }
    #endregion
}
