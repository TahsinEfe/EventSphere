using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSphere.Models
{
    public partial class Event
    {
        [Key]
        public int EventId { get; set; }

        public int OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int EventTypeId { get; set; }

        public int EventStatusId { get; set; }

        public int? OrganizerUserId { get; set; }

        public int? MaxAttendees { get; set; }

        public bool IsPublic { get; set; }

        public DateTime? RegistrationDeadline { get; set; }

        public int AddressId { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        [ForeignKey("EventStatusId")]
        public virtual EventStatus EventStatus { get; set; } = null!;

        [ForeignKey("EventTypeId")]
        public virtual EventType EventType { get; set; } = null!;

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; } = null!;

        [ForeignKey("OrganizerUserId")]
        public virtual User? OrganizerUser { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
