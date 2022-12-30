using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("productId")]
    public int ProductId { get; set; }

    [Column("storeId")]
    public int StoreId { get; set; }

    [Column("productName")]
    [Unicode(false)]
    public string ProductName { get; set; } = null!;

    [Column("productImg")]
    [Unicode(false)]
    public string ProductImg { get; set; } = null!;

    [Column("productPrice")]
    [Unicode(false)]
    public string ProductPrice { get; set; } = null!;

    [Column("productDescription")]
    [Unicode(false)]
    public string ProductDescription { get; set; } = null!;

    [Column("isBestSeller")]
    public bool IsBestSeller { get; set; }

    [Column("inOffer")]
    public bool InOffer { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<BasketOrder> BasketOrders { get; } = new List<BasketOrder>();

    [InverseProperty("Product")]
    public virtual ICollection<Offer> Offers { get; } = new List<Offer>();

    [ForeignKey("StoreId")]
    [InverseProperty("Products")]
    public virtual Store? Store { get; set; } = null;
}
