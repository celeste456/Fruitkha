using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Product Product { get; set; } = null!;
}
