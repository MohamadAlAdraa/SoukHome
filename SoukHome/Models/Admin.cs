using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[PrimaryKey("AdminId", "Email")]
[Table("Admin")]
public partial class Admin
{
    [Key]
    [Column("adminId")]
    public int AdminId { get; set; }

    [Key]
    [Column("email")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; } = null!;

    [Column("firstName")]
    [Unicode(false)]
    public string? FirstName { get; set; } = null!;

    [Column("lastName")]
    [Unicode(false)]
    public string? LastName { get; set; } = null!;

    [Column("password")]
    [Unicode(false)]
    public string? Password { get; set; } = null!;

    [InverseProperty("Admin")]
    public virtual ICollection<Store> Stores { get; } = new List<Store>();
}
