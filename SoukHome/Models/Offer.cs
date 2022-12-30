using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoukHome.Models;

[PrimaryKey("OfferId", "ProductId")]
[Table("Offer")]
public partial class Offer
{
    [Key]
    [Column("offerId")]
    public int OfferId { get; set; }

    [Key]
    [Column("productId")]
    public int ProductId { get; set; }

    [Column("newPrice")]
    public int NewPrice { get; set; }

    [Column("offerPercentage")]
    public int OfferPercentage { get; set; }

    [Column("expirationDate")]
    [Unicode(false)]
    public string ExpirationDate { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Offers")]
    public virtual Product? Product { get; set; }
}
