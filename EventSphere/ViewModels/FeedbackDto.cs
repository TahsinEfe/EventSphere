namespace EventSphere.ViewModels
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? EventName { get; set; }
        public string? UserName { get; set; }
    }
}
