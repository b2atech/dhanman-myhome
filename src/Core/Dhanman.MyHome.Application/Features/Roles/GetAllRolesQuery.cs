using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Roles;

namespace Dhanman.MyHome.Application.Features.Roles;

public sealed class GetAllRolesQuery : IQuery<Result<RoleListResponse>> { }