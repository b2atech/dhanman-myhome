﻿using Dhanman.MyHome.Domain.Entities.Units;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IUnitRepository
{
    #region Methods
    Task<Unit?> GetBydIdIntAsync(int id);

    void Insert(Unit unit);

    void Delete(Unit unit);

    void Update(Unit unit);

    int GetTotalRecordsCount();

    Task<bool> IsFlatValidAsync(string name);
    Task<int> GetLastUnitIdAsync();
    #endregion
}
