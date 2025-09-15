using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Enums
{
    public enum FirebaseMessageType
    {
        // Approval flows
        GateApprovalRequest,
        GateApproved,
        GateRejected,

        // Resident-to-resident messages
        ResidentMessage,
        AppCall,

        // Emergency and system notifications
        EmergencyAlert,
        SystemAnnouncement,

        // Visitor flows
        VisitorArrived,
        VisitorCheckedOut,

        // Add more types as your app evolves
    }
}
