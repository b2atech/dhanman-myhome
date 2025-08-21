using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dhanman.MyHome.Domain.Entities.Units;

public class UnitInfo : EntityInt
{
    [Column("id")]
    public int Id { get; set; }
    [Column("unit_id")]
    public int UnitId { get; set; }

    [Column("unit_name")]
    public string UnitName { get; set; }

    [Column("user_name")]
    public string UserName { get; set; }

    [Column("apartment_id")]
    public Guid ApartmentId { get; set; }
}

