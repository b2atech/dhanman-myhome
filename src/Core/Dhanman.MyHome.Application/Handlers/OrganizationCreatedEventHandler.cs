//using B2aTech.CrossCuttingConcern.Abstractions;
//using B2aTech.CrossCuttingConcern.Core.Result;
//using B2aTech.CrossCuttingConcern.Messaging;
//using Dhanman.MyHome.Application.Abstractions.Data;
//using Dhanman.Shared.Contracts.Common;
//using Dhanman.MyHome.Application.Features.Organizations.Events;
//using Dhanman.Shared.Contracts.Events;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Npgsql;
//using NpgsqlTypes;

//namespace Dhanman.MyHome.Application.Handlers;

//public class OrganizationCreatedEventHandler : IEventMessageHandler<BasicOrganizationCreatedEvent>
//{
//    private readonly ILogger<OrganizationCreatedEventHandler> _logger;
//    private readonly IApplicationDbContext _dbContext;
//    private readonly IMediator _mediator;

//    public OrganizationCreatedEventHandler(ILogger<OrganizationCreatedEventHandler> logger,
//        IApplicationDbContext dbContext,
//        IMediator mediator)
//    {
//        _logger = logger;
//        _dbContext = dbContext;
//        _mediator = mediator;
//    }

//    public async Task HandleAsync(BasicOrganizationCreatedEvent @event, MessageContext context)
//    {
//        await _dbContext.Database.ExecuteSqlRawAsync(
//                    "CALL public.initialize_organization(@p_id,@p_name, @p_company_ids,@p_company_names, @p_user_id, @p_user_first_name, @p_user_last_name, @p_phone_number, @p_email, @p_created_by, @p_default_company_id)",

//                new NpgsqlParameter("p_id", NpgsqlDbType.Uuid) { Value = @event.BasicOrganization.OrganizationId },
//                new NpgsqlParameter("p_name", NpgsqlDbType.Text) { Value = @event.BasicOrganization.Name },
//                new NpgsqlParameter("p_company_ids", NpgsqlDbType.Text) { Value = string.Join(",", @event.BasicOrganization.CompanyIds) },
//                new NpgsqlParameter("p_company_names", NpgsqlDbType.Text) { Value = string.Join(",", @event.BasicOrganization.CompanyNames) },
//                new NpgsqlParameter("p_user_id", NpgsqlDbType.Uuid) { Value = @event.BasicOrganization.UserId },
//                new NpgsqlParameter("p_user_first_name", NpgsqlDbType.Text) { Value = @event.BasicOrganization.UserFirstName },
//                new NpgsqlParameter("p_user_last_name", NpgsqlDbType.Text) { Value = @event.BasicOrganization.UserLastName },
//                new NpgsqlParameter("p_phone_number", NpgsqlDbType.Varchar) { Value = @event.BasicOrganization.PhoneNumber },
//                new NpgsqlParameter("p_email", NpgsqlDbType.Varchar) { Value = @event.BasicOrganization.Email },
//                new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = @event.BasicOrganization.CreatedBy },
//                new NpgsqlParameter("p_default_company_id", NpgsqlDbType.Uuid) { Value = @event.BasicOrganization.DefaultCompanyId }
//            );

//        await _mediator.Publish(new InitializeOrganizationEvent(@event.BasicOrganization.OrganizationId));
//        _logger.LogInformation("OrganizationCreatedEvent for OrganizationId: {OrganizationId} processed Successfully...!!!", @event.BasicOrganization.OrganizationId);
//        Result.Success(new EntityCreatedResponse(@event.BasicOrganization.OrganizationId));
//    }
//}
