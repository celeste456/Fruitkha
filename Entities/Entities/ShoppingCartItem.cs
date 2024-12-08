﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
	public partial class ShoppingCartItem
	{
		public int Id { get; set; }
		public int ShoppingCartId { get; set; }
		public ShoppingCart ShoppingCart { get; set; } = null!;

		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;

		public decimal Quantity { get; set; }
	}
}
