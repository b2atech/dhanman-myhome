using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Contracts.Enums;
public enum VisitorStatus
{
    PENDING = 1,
    APPROVED = 2,
    REJECTED = 6,
    CANCELLED = 5,
    PARTIAL_APPROVED = 7,
    CHECKED_IN = 3,
    CHECKED_OUT = 4
}