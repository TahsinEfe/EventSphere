using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class Task
{
    [Key]
    [Column("TaskID")]
    public int TaskId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = null!;

    [Column("AssignedUserID")]
    public int? AssignedUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("TaskStatusID")]
    public int TaskStatusId { get; set; }

    [ForeignKey("AssignedUserId")]
    [InverseProperty("Tasks")]
    public virtual User? AssignedUser { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Tasks")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("TaskStatusId")]
    [InverseProperty("Tasks")]
    public virtual TaskStatus TaskStatus { get; set; } = null!;
}
