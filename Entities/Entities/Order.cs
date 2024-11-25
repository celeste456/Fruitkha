using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public int? DiscountCodeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DiscountCode? DiscountCode { get; set; }
}
