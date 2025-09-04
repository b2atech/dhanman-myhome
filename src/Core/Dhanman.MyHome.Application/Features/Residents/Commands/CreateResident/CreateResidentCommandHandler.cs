using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Constants.Enums;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Commands;
using Dhanman.Shared.Contracts.Common;
using Dhanman.Shared.Contracts.Events;
using Dhanman.Shared.Contracts.Routing;
using MediatR;
using System.Threading.Tasks;
using ResidentAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IPurchaseServiceClient _purchaseServiceClient;
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    private readonly ICommandPublisher _commandPublisher;

    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IAddressRepository addressRepository, IResidentUnitRepository residentUnitRepository, IUserRepository userRepository, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient, IMediator mediator, IApplicationDbContext dbContext, IUnitOfWork unitOfWork,IUserContextService userContextService, ICommandPublisher commandPublisher)
    {
        _residentRepository = residentRepository;
        _addressRepository = addressRepository;
        _residentUnitRepository = residentUnitRepository;
        _userRepository = userRepository;
        _commonServiceClient = commonServiceClient;
        _purchaseServiceClient = purchaseServiceClient;
        _salesServiceClient = salesServiceClient;
        _mediator = mediator;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _userContextService = userContextService;
        _commandPublisher = commandPublisher;
    }
    #endregion

    #region Methodes

    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
    {
        // 1. Find or create User by Email
        var (user, isNewUser) = await FindOrCreateUserByEmailAsync(request.Email, request);


        // 2. Find or create Resident
        var resident = await FindOrCreateResidentAsync(request, user.Id);

        // 3. Add ResidentUnit
        await AddResidentUnitAsync(request.UnitId, resident.Id);

        // 3a. Publish UserCreated command to Common service if User is newly created
        if (isNewUser) // User is newly created
        {
            var messageContext = new MessageContext
            {
                UserId = _userContextService.CurrentUserId,
                OrganizationId = _userContextService.OrganizationId,
                CorrelationId = Guid.NewGuid()
            };
            await PublishUserCreatedAsync(user.Id, request.ApartmentId, request.FirstName, request.LastName, request.Email, request.ContactNumber, messageContext);
        }
        // 4. Publish domain event
        await _mediator.Publish(new ResidentCreatedEvent(resident.Id), cancellationToken);

        // 5. Return result
        return Result.Success(new EntityCreatedResponse(resident.Id));
    }

    // Private helpers

    private async Task<(User user, bool isNew)> FindOrCreateUserByEmailAsync(string email, CreateResidentCommand request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
            return (existingUser, false);

        // Create new User
        var newUser = new User(
            Guid.NewGuid(),
            request.ApartmentId,
            new Domain.Entities.Users.FirstName(request.FirstName),
            new Domain.Entities.Users.LastName(request.LastName),
            new Domain.Entities.Users.Email(request.Email),
            new ContactNumber(request.ContactNumber),
            request.ResidentTypeId == (int)ResidentType.OWNER
        );
        _userRepository.Insert(newUser);
        await _unitOfWork.SaveChangesAsync();
        return (newUser, true);
    }

    private async Task<Resident> FindOrCreateResidentAsync(CreateResidentCommand request, Guid userId)
    {
        var existingResident = _residentRepository.GetByEmail(request.Email, request.ApartmentId);
        if (existingResident != null)
            return existingResident;

        Guid? permanentAddressId = null;
        if (request.PermanentAddress != null)
        {
            Guid cityId = await GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);
            var permanentAddress = GetAddress(request.PermanentAddress, cityId);
            _addressRepository.Insert(permanentAddress);
            permanentAddressId = permanentAddress.Id;
        }

        var resident = new Resident(
            request.ApartmentId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.ContactNumber,
            permanentAddressId,
            userId,
            request.ResidentTypeId,
            request.OccupancyStatusId
        );
        _residentRepository.Insert(resident);
        await _unitOfWork.SaveChangesAsync();
        return resident;
    }

    private async Task AddResidentUnitAsync(int unitId, int residentId)
    {
        var residentUnit = new ResidentUnit(unitId, residentId);
        _residentUnitRepository.Insert(residentUnit);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task PublishUserCreatedAsync(Guid userId, Guid apartmentId, string firstName, string lastName, string email, string contactNumber, MessageContext messageContext)
    {
        var userCommand = new CreateUserCommand(userId, apartmentId, firstName, lastName, email, contactNumber, messageContext);

        var eventEnvelope = new CommandEnvelope<CreateUserCommand>
        {
            CommandType = RoutingKeys.Community.CreateUserInCommonAfterResident,
            Source = "CommunityService",
            UserId = messageContext.UserId,
            OrganizationId = messageContext.OrganizationId,
            CorrelationId = messageContext.CorrelationId,
            Payload = userCommand
        };

        await _commandPublisher.PublishAsync(RoutingKeys.Community.CreateUserInCommonAfterResident, eventEnvelope);
    }

    private async Task<Guid> GetCityId(string cityName, string zipCode, Guid stateId)
    {
        string lowerCityName = cityName.ToLower();
        var city = _dbContext.Set<City>().FirstOrDefault(x => x.Name.ToLower() == lowerCityName && x.ZipCode == zipCode);

        if (city == null)
        {
            city = new City(Guid.NewGuid(), stateId, zipCode, cityName);
            _dbContext.Set<City>().Add(city);
            await _unitOfWork.SaveChangesAsync();
        }
        return city.Id;
    }

    private Address GetAddress(ResidentAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }

    #endregion

}