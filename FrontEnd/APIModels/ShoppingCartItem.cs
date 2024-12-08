namespace FrontEnd.APIModels
{
	public class ShoppingCartItem
	{
		public int Id { get; set; }
		public int ShoppingCartId { get; set; }
		public int ProductId { get; set; }
		public decimal Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
