public class EventDto
{
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
    public string? ImageUrl { get; set; }  
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
