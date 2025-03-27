using Dhanman.MyHome.Domain.Entities.ResidentTokens;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IResidentTokenRepository
{
    Task<ResidentToken?> GetByResidentIdAsync(int id);
    void Insert(ResidentToken address);
    void Update(ResidentToken address);
}
