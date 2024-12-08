using Entities.Entities;

namespace BackEnd.DTO
{
	public class ShoppingCartDTO
	{
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public ICollection<ShoppingCartItemDTO> Items { get; set; } = new List<ShoppingCartItemDTO>();
	}
}