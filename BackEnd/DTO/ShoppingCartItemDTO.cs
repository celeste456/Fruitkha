using Entities.Entities;

namespace BackEnd.DTO
{
	public class ShoppingCartItemDTO
	{
		public int Id { get; set; } 
		public int ShoppingCartId { get; set; } 
		public int ProductId { get; set; } 
		public decimal Quantity { get; set; } 
		public decimal TotalPrice { get; set; }
	}
}
