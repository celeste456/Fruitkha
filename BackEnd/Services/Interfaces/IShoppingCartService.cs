using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
	public interface IShoppingCartService
	{
		ShoppingCartDTO GetCartByUserId(string userId);
		bool AddItemToCart(string userId, ShoppingCartItemDTO item);
		bool UpdateItemInCart(string userId, ShoppingCartItemDTO item);
		bool RemoveItemFromCart(string userId, int itemId);
		decimal GetCartTotal(string userId);
	}
}
