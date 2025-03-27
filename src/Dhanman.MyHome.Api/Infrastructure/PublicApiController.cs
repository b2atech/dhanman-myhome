using B2aTech.CrossCuttingConcern.Core.Primitives;
using Dhanman.MyHome.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Infrastructure;

public abstract class PublicApiController : ControllerBase
{
    #region Properties
    protected IMediator Mediator { get; }   
    #endregion

    #region Constructors
    protected PublicApiController(IMediator mediator) 
    {
        Mediator = mediator;       
    }
    #endregion

    #region Methodes

    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));

    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
    protected IActionResult NotFound(Error error)
    {
        // This method was created so that it is easier to match the extension methods on the Result class.
        return NotFound();
    }

    #endregion

}