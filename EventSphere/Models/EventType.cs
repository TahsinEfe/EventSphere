using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

[Index("TypeName", Name = "UQ__EventTyp__D4E7DFA8C68F95AB", IsUnique = true)]
public partial class EventType
{
    [Key]
    [Column("EventTypeID")]
    public int EventTypeId { get; set; }

    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    [InverseProperty("EventType")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
