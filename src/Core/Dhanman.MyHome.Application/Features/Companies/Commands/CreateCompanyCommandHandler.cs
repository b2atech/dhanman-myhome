﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Companies.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Companies;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Companies.Commands;

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly ICompanyRepository _companyRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMediator mediator)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company(request.CompanyId, request.OrganizationId, request.Name, request.IsApartment);

        _companyRepository.Insert(company);

        await _mediator.Publish(new CompanyCreatedEvent(company.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(company.Id));
    }
    #endregion
}
