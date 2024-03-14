using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1Test.Models.EntityFramework;

[Table("product_order")]
[Index("ProductOrderId", Name = "product_order_id_UNIQUE", IsUnique = true)]
public partial class ProductOrder
{
    [Key]
    [Column("product_order_id")]
    public int ProductOrderId { get; set; }

    [Column("order_id")]
    [StringLength(45)]
    public string OrderId { get; set; } = null!;

    [Column("product_id")]
    [StringLength(45)]
    public string? ProductId { get; set; }
}
