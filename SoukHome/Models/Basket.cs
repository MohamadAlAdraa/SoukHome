using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[PrimaryKey("BasketId", "CustomerBasketEmailId")]
[Table("Basket")]
public partial class Basket
{
    [Key]
    [Column("basketId")]
    public int BasketId { get; set; }

    [Key]
    [Column("customerBasketEmailId")]
    [StringLength(200)]
    [Unicode(false)]
    public string CustomerBasketEmailId { get; set; } = null!;

    [InverseProperty("Basket")]
    public virtual ICollection<BasketOrder> BasketOrders { get; } = new List<BasketOrder>();

    [ForeignKey("BasketId, CustomerBasketEmailId")]
    [InverseProperty("Basket")]
    public virtual Customer? Customer { get; set; } = null;
}
