using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class DiscountCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public decimal DiscountPercentage { get; set; }

    public byte[]? Photo { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
