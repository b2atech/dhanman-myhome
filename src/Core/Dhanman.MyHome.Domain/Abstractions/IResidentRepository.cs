﻿using Dhanman.MyHome.Domain.Entities.Residents;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IResidentRepository
{
    #region Methods
    Task<Resident?> GetBydIdIntAsync(int id);

    void Insert(Resident resident);

    void Delete(Resident resident);

    void Update(Resident resident);

    int GetTotalRecordsCount();

    Resident? GetByEmail(string email, Guid ApartmentId);

    #endregion

}