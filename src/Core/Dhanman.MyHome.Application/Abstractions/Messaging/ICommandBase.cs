﻿using MediatR;

namespace Dhanman.MyHome.Application.Abstractions.Messaging;

public interface ICommand : IRequest, ICommandBase
{
}

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{
}

public interface ICommandBase
{
}