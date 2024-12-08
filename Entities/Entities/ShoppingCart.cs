using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class ShoppingCart
{
	public int Id { get; set; }
	public string UserId { get; set; } = null!;
	public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
}
