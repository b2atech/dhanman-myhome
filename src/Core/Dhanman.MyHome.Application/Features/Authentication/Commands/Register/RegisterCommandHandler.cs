using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Features.Authentication.Commands.Register;

internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<EntityCreatedResponse>>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(),
                            FirstName.Create(request.FirstName).Value(),
                            LastName.Create(request.LastName).Value(),
                    
                            Email.Create(request.Email).Value());

        _userRepository.Insert(user);

        return Result.Success(new EntityCreatedResponse(user.Id));
    }

}
