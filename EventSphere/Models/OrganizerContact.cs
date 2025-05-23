using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class OrganizerContact
{
    [Key]
    [Column("ContactID")]
    public int ContactId { get; set; }

    [Column("OrganizationID")]
    public int OrganizationId { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(255)]
    public string? SocialMedia { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("OrganizerContacts")]
    public virtual Organization Organization { get; set; } = null!;
}
