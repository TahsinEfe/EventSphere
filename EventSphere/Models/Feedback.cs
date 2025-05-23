using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("FeedbackID")]
    public long FeedbackId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SubmissionDate { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Feedbacks")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Feedbacks")]
    public virtual User? User { get; set; }
}
