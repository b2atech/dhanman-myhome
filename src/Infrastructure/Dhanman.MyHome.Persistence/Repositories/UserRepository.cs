using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> IsEmailUniqueAsync(string email)
         => !await _dbContext.Set<User>().AnyAsync(user => user.Email.Value == email);

    public async Task<User?> GetByEmailAsync(string email)
         => await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email.Value == email);

    public void Insert(User user) => _dbContext.Set<User>().Add(user);
}
