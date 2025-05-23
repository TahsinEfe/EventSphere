using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class Event
{
    [Key]
    [Column("EventID")]
    public int EventId { get; set; }

    [Column("OrganizationID")]
    public int OrganizationId { get; set; }

    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDateTime { get; set; }

    [Column("EventTypeID")]
    public int EventTypeId { get; set; }

    [Column("EventStatusID")]
    public int EventStatusId { get; set; }

    [Column("OrganizerUserID")]
    public int? OrganizerUserId { get; set; }

    public int? MaxAttendees { get; set; }

    public bool IsPublic { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegistrationDeadline { get; set; }

    [StringLength(200)]
    public string? Location { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

    [ForeignKey("EventStatusId")]
    [InverseProperty("Events")]
    public virtual EventStatus EventStatus { get; set; } = null!;

    [ForeignKey("EventTypeId")]
    [InverseProperty("Events")]
    public virtual EventType EventType { get; set; } = null!;

    [InverseProperty("Event")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [ForeignKey("OrganizationId")]
    [InverseProperty("Events")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("OrganizerUserId")]
    [InverseProperty("Events")]
    public virtual User? OrganizerUser { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [InverseProperty("Event")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
