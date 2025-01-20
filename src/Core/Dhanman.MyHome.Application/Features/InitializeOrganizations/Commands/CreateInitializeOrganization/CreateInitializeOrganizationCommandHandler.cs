using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.InitializeOrganizations.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;


namespace Dhanman.MyHome.Application.Features.InitializeOrganizations.Commands.CreateInitializeOrganization;

public class CreateInitializeOrganizationCommandHandler : ICommandHandler<CreateInitializeOrganizationCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    IUserContextService _userContextService;
    #endregion

    #region Constructors
    public CreateInitializeOrganizationCommandHandler(IApplicationDbContext dbContext, IMediator mediator, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _userContextService = userContextService;
    }

    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateInitializeOrganizationCommand request, CancellationToken cancellationToken)
    {

        await _dbContext.Database.ExecuteSqlRawAsync(
                    "CALL public.initialize_organization(@p_id,@p_name, @p_company_guids,@p_company_names, @p_user_id, @p_user_first_name, @p_user_last_name,@p_phone_number,@p_email, @p_created_by)",

                new NpgsqlParameter("p_id", NpgsqlDbType.Uuid) { Value = request.Id },
                new NpgsqlParameter("p_name", NpgsqlDbType.Text) { Value = request.Name },
                new NpgsqlParameter("p_company_guids", NpgsqlDbType.Text) { Value = string.Join(",", request.CompanyGuids) },
                new NpgsqlParameter("p_company_names", NpgsqlDbType.Text) { Value = string.Join(",", request.CompanyNames) },
                new NpgsqlParameter("p_user_id", NpgsqlDbType.Uuid) { Value = request.UserId },
                new NpgsqlParameter("p_user_first_name", NpgsqlDbType.Text) { Value = request.UserFirstName },
                new NpgsqlParameter("p_user_last_name", NpgsqlDbType.Text) { Value = request.UserLastName },
                new NpgsqlParameter("p_phone_number", NpgsqlDbType.Varchar) { Value = request.Email },
                new NpgsqlParameter("p_email", NpgsqlDbType.Varchar) { Value = request.PhoneNumber },
                new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = _userContextService.GetCurrentUserId() }
            );

        await _mediator.Publish(new InitializeOrganizationCreatedEvent(request.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(request.Id));

    }

    #endregion
}
