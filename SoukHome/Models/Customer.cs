using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[PrimaryKey("CustomerId", "Email")]
[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("customerId")]
    public int CustomerId { get; set; }

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

    [Column("phoneNumber")]
    [Unicode(false)]
    public string? PhoneNumber { get; set; } = null!;

    [Column("address")]
    [Unicode(false)]
    public string? Address { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual Basket? Basket { get; set; }
}
