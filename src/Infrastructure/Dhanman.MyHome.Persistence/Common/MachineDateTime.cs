using Dhanman.MyHome.Application.Abstractions.Data;

namespace Dhanman.MyHome.Persistence.Common;

internal sealed class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
