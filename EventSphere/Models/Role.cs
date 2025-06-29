﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Models;

[Index("RoleName", Name = "UQ__Roles__8A2B6160F6C7A0C3", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
