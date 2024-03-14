using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1Test.Models.EntityFramework;

[Table("order")]
[Index("OrderId", Name = "OrderID_UNIQUE", IsUnique = true)]
public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [StringLength(45)]
    public string? OrderName { get; set; }

    [StringLength(45)]
    public string? OrderDescription { get; set; }

    [Column("order_by")]
    [StringLength(45)]
    public string? OrderBy { get; set; }
}
