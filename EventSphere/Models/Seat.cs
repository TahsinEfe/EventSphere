using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class Seat
{
    [Key]
    [Column("SeatID")]
    public int SeatId { get; set; }

    [Column("EventID")]
    public int EventId { get; set; }

    [StringLength(100)]
    public string? Section { get; set; }

    [StringLength(10)]
    public string? RowNumber { get; set; }

    [StringLength(10)]
    public string? SeatNumber { get; set; }

    public bool IsReserved { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Seats")]
    public virtual Event Event { get; set; } = null!;
}
