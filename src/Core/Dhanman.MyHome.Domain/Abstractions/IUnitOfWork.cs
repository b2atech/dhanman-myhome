namespace Dhanman.MyHome.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Allows saving with an explicitly specified userId (e.g., for background/event scenarios).
    /// </summary>
    Task<int> SaveChangesAsync(Guid userId,CancellationToken cancellationToken = default);
}
