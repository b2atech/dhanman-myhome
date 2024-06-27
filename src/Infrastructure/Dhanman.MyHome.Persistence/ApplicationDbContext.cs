using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Dhanman.MyHome.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    #region Properties
    private readonly IDateTime _dateTime;
    #endregion

    #region Constructors
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        _dateTime = dateTime;
    }
    #endregion

    #region Methodes
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);

    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime utcNow = _dateTime.UtcNow;

        UpdateAuditableEntities(utcNow);

        UpdateSoftDeletableEntities(utcNow);

        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow.SetKindUtc();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow.SetKindUtc();
            }
        }
    }

    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow.SetKindUtc();

                entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
            }
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }
    #endregion

    #region Guid Based Entities
    public new DbSet<TEntity> Set<TEntity>()
           where TEntity : Entity =>
           base.Set<TEntity>();

    public async Task<TEntity?> GetBydIdAsync<TEntity>(Guid id)
           where TEntity : Entity
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity =>
            Set<TEntity>().Add(entity);
    public void Update<TEntity>(TEntity entity)
        where TEntity : Entity =>
        Set<TEntity>().Update(entity);

    public new void Remove<TEntity>(TEntity entity)
            where TEntity : Entity =>
            Set<TEntity>().Remove(entity);

    #endregion

    #region Int Based Entities
    public new DbSet<TEntity> SetInt<TEntity>()
           where TEntity : EntityInt =>
           base.Set<TEntity>();

    public async Task<TEntity?> GetByIdIntAsync<TEntity>(int id)
           where TEntity : EntityInt
    {
        return await SetInt<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public void InsertInt<TEntity>(TEntity entity)
            where TEntity : EntityInt =>
            SetInt<TEntity>().Add(entity);
    public void UpdateInt<TEntity>(TEntity entity)
        where TEntity : EntityInt =>
        SetInt<TEntity>().Update(entity);

    public new void RemoveInt<TEntity>(TEntity entity)
            where TEntity : EntityInt =>
            SetInt<TEntity>().Remove(entity);
    #endregion
}
