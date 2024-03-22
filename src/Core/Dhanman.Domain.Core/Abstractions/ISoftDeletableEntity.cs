using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.Domain.Core.Abstractions
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }
        bool IsDeleted { get; }
    }
}
