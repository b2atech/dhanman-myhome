using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Authentication;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Authentication;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Abstractions;

namespace Dhanman.MyHome.Application.Features.Authentication.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="passwordHashChecker">The password hash checker.</param>
    /// <param name="jwtProvider">The JWT provider.</param>
    public LoginCommandHandler(IUserRepository userRepository,IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken) =>
            await Result.Success(request)
                .Bind(
                    command => _userRepository.GetByEmailAsync(command.Email),
                    Errors.Authentication.InvalidEmailOrPassword)
                .Ensure(
                    user => user.VerifyPasswordHash(request.Password),
                    Errors.Authentication.InvalidEmailOrPassword)
                .Bind(user => _jwtProvider.CreateAsync(user));
}