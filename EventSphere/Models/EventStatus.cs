using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

[Index("StatusName", Name = "UQ__EventSta__05E7698A3684046D", IsUnique = true)]
public partial class EventStatus
{
    [Key]
    [Column("EventStatusID")]
    public int EventStatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("EventStatus")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
