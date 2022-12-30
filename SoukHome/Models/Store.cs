using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[Table("Store")]
public partial class Store
{
    [Key]
    [Column("storeId")]
    public int StoreId { get; set; }

    [Column("email")]
    [StringLength(200)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("adminId")]
    public int AdminId { get; set; }

    [Column("storeName")]
    [Unicode(false)]
    public string StoreName { get; set; } = null!;

    [Column("storeOwner")]
    [Unicode(false)]
    public string StoreOwner { get; set; } = null!;

    [Column("storeLocation")]
    [Unicode(false)]
    public string StoreLocation { get; set; } = null!;

    [Column("storeLogo")]
    [Unicode(false)]
    public string StoreLogo { get; set; } = null!;

    [ForeignKey("AdminId, Email")]
    [InverseProperty("Stores")]
    public virtual Admin? Admin { get; set; } = null;

    [InverseProperty("Store")]
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
