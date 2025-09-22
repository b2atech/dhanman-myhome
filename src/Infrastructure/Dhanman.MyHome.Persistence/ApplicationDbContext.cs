using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.CommitteeMembers;
using Dhanman.MyHome.Domain.Entities.Portfolios;
using Dhanman.MyHome.Domain.Entities.Roles;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Vehicals;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.MyHome.Persistence.Constants;
using Dhanman.MyHome.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Dhanman.MyHome.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    #region Properties
    private readonly IDateTime _dateTime;
    private readonly IUserContextService _userContextService;
    private Guid? _overrideUserId;

    #endregion

    #region Constructors
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime, IUserContextService userContextService)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        _dateTime = dateTime;
        _userContextService = userContextService;
    }
    #endregion

    #region Methodes
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<VisitorApprovalInfoById>().ToTable(TableNames.VisitorApprovalInfoById, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<AllVisitorLog>().ToTable(TableNames.AllVisitorLog, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<VisitorDbDto>().ToTable(TableNames.VisitorDbDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<CommitteeAllMemberDto>().ToTable(TableNames.CommitteeAllMemberDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<CommitteeMemberDto>().ToTable(TableNames.CommitteeMemberDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<PortfolioDto>().ToTable(TableNames.PortfolioDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<RoleDto>().ToTable(TableNames.RoleDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<UnitInfo>().ToTable(TableNames.UnitInfo, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<VehicleInfo>().ToTable(TableNames.VehicleInfo, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<ResidentNames>().ToTable(TableNames.ResidentNames, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<UnitOwnerNameEntity>().ToTable(TableNames.UnitOwnerNameEntity, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<UnitNamesByAparmentEntity>().ToTable(TableNames.UnitNamesByAparmentEntity, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<VisitorByContactDto>().ToTable(TableNames.VisitorByContactDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<VisitorUserIdsDto>().ToTable(TableNames.VisitorUserIdsDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<VisitorNotificationDataDto>().ToTable(TableNames.VisitorNotificationDataDto, t => t.ExcludeFromMigrations());
        modelBuilder.Entity<UpdateVisitorStatusDto>().ToTable(TableNames.UpdateVisitorStatusDto, t => t.ExcludeFromMigrations());


        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);

    }

    public Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _overrideUserId = userId;
        var result = SaveChangesAsync(cancellationToken); // Calls the main override
        _overrideUserId = null;
        return result;
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
        var currentUserId = _overrideUserId ?? _userContextService?.CurrentUserId;
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow.SetKindUtc();
                entityEntry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = currentUserId;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow.SetKindUtc();
                entityEntry.Property(nameof(IAuditableEntity.ModifiedBy)).CurrentValue = currentUserId;
            }
        }
    }

    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        var currentUserId = _overrideUserId ?? _userContextService?.CurrentUserId;
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow.SetKindUtc();

                entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;
                entityEntry.Property(nameof(IAuditableEntity.ModifiedBy)).CurrentValue = currentUserId;


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

    public async Task<TEntity?> GetBydIdIntAsync<TEntity>(int id)
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
