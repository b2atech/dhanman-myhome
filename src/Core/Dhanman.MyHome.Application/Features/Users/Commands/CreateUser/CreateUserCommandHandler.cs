using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Users.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;
using MediatR;
using Dhanman.Shared.Contracts.Commands;

namespace Dhanman.MyHome.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var existingVendor = await _userRepository.GetByIdAsync(request.UserId);
        if (existingVendor is not null)
        {
            return Result.Success(new EntityCreatedResponse(request.UserId));
        }

        else
        {
            var user = new User(request.UserId, request.CompanyId,new FirstName(request.FirstName), new LastName(request.LastName), new Email(request.Email), new ContactNumber(request.PhoneNumber));

            _userRepository.Insert(user);

            await _unitOfWork.SaveChangesAsync(request.MessageContext.UserId, cancellationToken);

            await _mediator.Publish(new UserCreatedEvent(user.Id), cancellationToken);

            return Result.Success(new EntityCreatedResponse(user.Id));
        }
    }
    #endregion
}
