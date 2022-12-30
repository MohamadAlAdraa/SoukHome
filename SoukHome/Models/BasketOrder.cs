using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[Table("BasketOrder")]
public partial class BasketOrder
{
    [Key]
    [Column("orderId")]
    public int OrderId { get; set; }

    [Column("basketId")]
    public int BasketId { get; set; }

    [Column("customerBasketEmailId")]
    [StringLength(200)]
    [Unicode(false)]
    public string CustomerBasketEmailId { get; set; } = null!;

    [Column("productId")]
    public int ProductId { get; set; }

    [Column("orderState")]
    [Unicode(false)]
    public string? OrderState { get; set; }

    [Column("date")]
    [Unicode(false)]
    public string? Date { get; set; }

    [ForeignKey("BasketId, CustomerBasketEmailId")]
    [InverseProperty("BasketOrders")]
    public virtual Basket? Basket { get; set; } = null;

    [ForeignKey("ProductId")]
    [InverseProperty("BasketOrders")]
    public virtual Product? Product { get; set; } = null;
}
