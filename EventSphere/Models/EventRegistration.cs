using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class EventRegistration
{
    [Key]
    [Column("RegistrationID")]
    public long RegistrationId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RegistrationDate { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("EventRegistrations")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("EventRegistrations")]
    public virtual User User { get; set; } = null!;
}
