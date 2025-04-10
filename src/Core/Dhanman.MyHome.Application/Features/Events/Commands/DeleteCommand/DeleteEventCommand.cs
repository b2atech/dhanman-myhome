using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.DeleteCommand;

public record class DeleteEventCommand(Guid id):ICommand<Result<EntityDeletedResponse>>;
