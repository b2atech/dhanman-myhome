using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.DeleteCommand;

public record class DeleteEventCommand(Guid id):ICommand<Result<EntityDeletedResponse>>;
