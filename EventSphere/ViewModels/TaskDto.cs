public class TaskDto
{
    public int TaskId { get; set; }
    public int? EventId { get; set; }
    public string Title { get; set; } = null!;
    public int? AssignedUserId { get; set; }
    public DateTime? DueDate { get; set; }
    public int? TaskStatusId { get; set; }
}
