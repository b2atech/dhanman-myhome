using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MediatR;
using Dhanman.MyHome.Api.Controllers;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Api.Tests.Controllers
{
    public class ServiceProvidersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUserContextService> _userContextServiceMock;
        private readonly ServiceProvidersController _controller;

        public ServiceProvidersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _userContextServiceMock = new Mock<IUserContextService>();
            _controller = new ServiceProvidersController(_mediatorMock.Object, _userContextServiceMock.Object);
        }

        [Fact]
        public async Task CheckinServiceProvider_ValidRequest_ReturnsOk()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new CheckinServiceProviderRequest("123456");
            var expectedResponse = new EntityCreatedResponse(1);

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(expectedResponse));

            // Act
            var result = await _controller.CheckinServiceProvider(apartmentId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task CheckinServiceProvider_NullRequest_ReturnsBadRequest()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            CheckinServiceProviderRequest? request = null;

            // Act
            var result = await _controller.CheckinServiceProvider(apartmentId, request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}