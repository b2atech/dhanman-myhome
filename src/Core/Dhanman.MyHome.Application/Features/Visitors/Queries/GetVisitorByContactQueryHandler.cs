using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries
{
    public sealed class GetVisitorByContactQueryHandler
     : IQueryHandler<GetVisitorByContactQuery, Result<VisitorByContactListResponse?>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetVisitorByContactQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<VisitorByContactListResponse?>> Handle(GetVisitorByContactQuery request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT * FROM public.get_visitor_by_contact(@p_apartment_id, @p_contact_number)";

            var parameters = new[]
            {
            new NpgsqlParameter("p_apartment_id", request.ApartmentId),
            new NpgsqlParameter("p_contact_number", request.ContactNumber)
        };

            var dto = await _dbContext.SetInt<VisitorByContactDto>()
                .FromSqlRaw(sql, parameters)
                .AsNoTracking()
                .Select(e => new VisitorByContactResponse(
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        e.Email,
                        e.ContactNumber,
                        e.VisitorTypeId,
                        e.VehicleNumber,
                        e.IdentityTypeId,
                        e.IdentityNumber))
                .ToListAsync(cancellationToken);

            

            var visitorListResponse = new VisitorByContactListResponse(dto);
            return visitorListResponse;
        }
    }
}
