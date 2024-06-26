//using B2aTech.CrossCuttingConcern.Abstractions;
//using B2aTech.CrossCuttingConcern.Core.Result;
//using Dhanman.MyHome.Api.Contracts;
//using Dhanman.MyHome.Api.Infrastructure;
//using Dhanman.MyHome.Application.Contracts.Authentication;
//using Dhanman.MyHome.Domain;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Dhanman.MyHome.Api.Controllers
//{
//    [AllowAnonymous]
//    public class AuthenticationController : ApiController
//    {
//        public AuthenticationController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
//        {
//        }

//        [HttpPost(ApiRoutes.Authentication.Register)]
//        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Register([FromBody] RegisterRequest? request) =>
//            await Result.Create(request, Errors.General.BadRequest)
//                .Map(value => new RegisterCommand(
//                    value.FirstName,
//                    value.LastName,
//                    value.Email,
//                    value.Password,
//                    value.ConfirmPassword))
//                .Bind(command => Mediator.Send(command))
//                .Match(Ok, BadRequest);

//        [HttpPost(ApiRoutes.Authentication.Login)]
//        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Login([FromBody] LoginRequest? request) =>
//            await Result.Create(request, Errors.General.BadRequest)
//                .Map(value => new LoginCommand(value.Email, value.Password))
//                .Bind(command => Mediator.Send(command))
//                .Match(Ok, BadRequest);
//    }
//}
