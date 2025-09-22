using Moq;
using Xunit;
using Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CheckinServiceProvider;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.ServiceProviderLogs;
using Dhanman.MyHome.Domain;

namespace Dhanman.MyHome.Api.Tests.ServiceProviders
{
    public class CheckinServiceProviderCommandHandlerTests
    {
        private readonly Mock<IServiceProviderRepository> _serviceProviderRepositoryMock;
        private readonly Mock<IServiceProviderLogRepository> _serviceProviderLogRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CheckinServiceProviderCommandHandler _handler;

        public CheckinServiceProviderCommandHandlerTests()
        {
            _serviceProviderRepositoryMock = new Mock<IServiceProviderRepository>();
            _serviceProviderLogRepositoryMock = new Mock<IServiceProviderLogRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            
            _handler = new CheckinServiceProviderCommandHandler(
                _serviceProviderRepositoryMock.Object,
                _serviceProviderLogRepositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var pin = "123456";
            var command = new CheckinServiceProviderCommand(apartmentId, pin);

            var serviceProvider = new ServiceProvider(
                "John", "Doe", "john@example.com", "Main Gate", "1234567890",
                Guid.NewGuid(), Guid.NewGuid(), 1, 1, "ABC123", 1, "ID123",
                DateTime.Now.AddYears(1), true, true, true, true, apartmentId, pin);

            _serviceProviderRepositoryMock
                .Setup(x => x.GetByApartmentIdAndPinAsync(apartmentId, pin))
                .ReturnsAsync(serviceProvider);

            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _serviceProviderLogRepositoryMock.Verify(x => x.Insert(It.IsAny<ServiceProviderLog>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ServiceProviderNotFound_ReturnsFailure()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var pin = "123456";
            var command = new CheckinServiceProviderCommand(apartmentId, pin);

            _serviceProviderRepositoryMock
                .Setup(x => x.GetByApartmentIdAndPinAsync(apartmentId, pin))
                .ReturnsAsync((ServiceProvider?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.ServiceProvider.NotFound.Code, result.Error.Code);
            _serviceProviderLogRepositoryMock.Verify(x => x.Insert(It.IsAny<ServiceProviderLog>()), Times.Never);
        }
    }
}