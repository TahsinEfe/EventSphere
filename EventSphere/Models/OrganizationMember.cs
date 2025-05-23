using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class OrganizationMember
{
    [Key]
    [Column("MemberID")]
    public int MemberId { get; set; }

    [Column("OrganizationID")]
    public int OrganizationId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime JoinDate { get; set; }

    public bool IsAdmin { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("OrganizationMembers")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OrganizationMembers")]
    public virtual User User { get; set; } = null!;
}
