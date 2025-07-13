using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DayOff.Persistence.Entities;

[Table("DY_DAY_OFF_TYPES", Schema = "C##USER1")]
public class Dy_DayOffType
{
    [Key]
    [Column("DY_OFF_ID")]
    public decimal DyOffId { get; set; }

    [Column("DY_OFF_NAME")]
    public string? DyOffName { get; set; }

    [Column("IS_GENDER_SPECIFIC")]
    public bool? IsGenderSpecific { get; set; }

    [Column("ALLOWED_GENDER_ID")]
    public decimal? AllowedGenderId { get; set; }

    [Column("IS_PARTIAL_ALLOWED")]
    public bool? IsPartialAllowed { get; set; }

    public virtual Dy_Gender? AllowedGender { get; set; }

    public virtual ICollection<Dy_DayOffPolicy> DyDayOffPolicies { get; set; } = new List<Dy_DayOffPolicy>();
    public virtual ICollection<Dy_DayOffRequest> DyDayOffRequests { get; set; } = new List<Dy_DayOffRequest>();

}
