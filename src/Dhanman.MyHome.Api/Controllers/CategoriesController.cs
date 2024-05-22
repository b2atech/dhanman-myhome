using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Catergories;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.SubCategories;
using Dhanman.MyHome.Application.Features.Categories.Queries;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;
using Dhanman.MyHome.Application.Features.Events.Queries;
using Dhanman.MyHome.Application.Features.SubCategories;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class CategoriesController: ApiController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }
    #region Category
   

    [HttpGet(ApiRoutes.Category.GetAllCategory)]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCategory() =>
    await Result.Success(new GetAllCategoryQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region SubCategory


    [HttpGet(ApiRoutes.SubCategory.GetAllSubCategory)]
    [ProducesResponseType(typeof(SubCategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllSubCategory() =>
    await Result.Success(new GetAllSubCategoryQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
}
