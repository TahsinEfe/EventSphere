using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

public partial class Organization
{
    [Key]
    [Column("OrganizationID")]
    public int OrganizationId { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? ContactEmail { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [InverseProperty("Organization")]
    public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();

    [InverseProperty("Organization")]
    public virtual ICollection<OrganizerContact> OrganizerContacts { get; set; } = new List<OrganizerContact>();
}
