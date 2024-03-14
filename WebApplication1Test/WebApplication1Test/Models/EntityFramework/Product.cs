using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1Test.Models.EntityFramework;

[Table("product")]
[Index("ProductId", Name = "ProductID_UNIQUE", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("product_ID")]
    public int ProductId { get; set; }

    [Column("Product_name")]
    [StringLength(45)]
    public string? ProductName { get; set; }

    [Column("product_description")]
    [StringLength(45)]
    public string? ProductDescription { get; set; }
}
