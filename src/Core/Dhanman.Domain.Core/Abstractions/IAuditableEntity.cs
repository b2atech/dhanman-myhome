using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.Domain.Core.Abstractions;

public interface IAuditableEntity
{
    Guid CreatedBy { get; }
    Guid? ModifiedBy { get; }
    DateTime CreatedOnUtc { get; }
    DateTime? ModifiedOnUtc { get; }
}
