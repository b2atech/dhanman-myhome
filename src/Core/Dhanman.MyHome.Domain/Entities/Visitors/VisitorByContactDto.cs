using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.Visitors
{
    public sealed class VisitorByContactDto : EntityInt
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = default!;

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("contact_number")]
        public string ContactNumber { get; set; } = default!;

        [Column("visitor_type_id")]
        public int VisitorTypeId { get; set; }

        [Column("vehicle_number")]
        public string? VehicleNumber { get; set; }

        [Column("identity_type_id")]
        public int? IdentityTypeId { get; set; }

        [Column("identity_number")]
        public string? IdentityNumber { get; set; }

        public VisitorByContactDto() { }

        public VisitorByContactDto(int id, string firstName, string? lastName, string? email,
            string contactNumber, int visitorTypeId, string? vehicleNumber,
            int? identityTypeId, string? identityNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNumber = contactNumber;
            VisitorTypeId = visitorTypeId;
            VehicleNumber = vehicleNumber;
            IdentityTypeId = identityTypeId;
            IdentityNumber = identityNumber;
        }
    }
}
