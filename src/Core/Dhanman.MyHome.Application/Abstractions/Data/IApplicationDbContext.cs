using B2aTech.CrossCuttingConcern.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Abstractions.Data;


public interface IApplicationDbContext
{
    #region Guid Based Entities
    DbSet<TEntity> Set<TEntity>()
           where TEntity : Entity;

    Task<TEntity?> GetBydIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

    void Insert<TEntity>(TEntity entity)
           where TEntity : Entity;

    void Remove<TEntity>(TEntity entity)
           where TEntity : Entity;

    void Update<TEntity>(TEntity entity)
       where TEntity : Entity;
    #endregion

    #region Int Based Entities
    DbSet<TEntity> SetInt<TEntity>()
           where TEntity : EntityInt;

    Task<TEntity?> GetBydIdIntAsync<TEntity>(int id)
            where TEntity : EntityInt;

    void InsertInt<TEntity>(TEntity entity)
           where TEntity : EntityInt;

    void RemoveInt<TEntity>(TEntity entity)
           where TEntity : EntityInt;

    void UpdateInt<TEntity>(TEntity entity)
       where TEntity : EntityInt;
    #endregion
}

