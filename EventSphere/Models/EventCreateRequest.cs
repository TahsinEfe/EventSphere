using Microsoft.AspNetCore.Http;

public class EventCreateRequest
{
    public string Name { get; set; }
    public string StartDateTime { get; set; }
    public required DateTime EndDateTime { get; set; }
    public int EventTypeId { get; set; }
    public int EventStatusId { get; set; }
    public int OrganizationId { get; set; }
    public int? OrganizerUserId { get; set; }
    public bool IsPublic { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public int? MaxAttendees { get; set; }
    public string? RegistrationDeadline { get; set; }


    public IFormFile? ImageFile { get; set; }
}
