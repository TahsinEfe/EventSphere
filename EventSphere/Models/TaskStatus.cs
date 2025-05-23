using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

[Index("StatusName", Name = "UQ__TaskStat__05E7698A3DA3F13E", IsUnique = true)]
public partial class TaskStatus
{
    [Key]
    [Column("TaskStatusID")]
    public int TaskStatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("TaskStatus")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
