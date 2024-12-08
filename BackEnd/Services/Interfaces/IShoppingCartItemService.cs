using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
	public interface IShoppingCartItemService
	{
		IEnumerable<ShoppingCartItemDTO> GetAll();
		ShoppingCartItemDTO GetById(int id);
		bool Create(ShoppingCartItemDTO itemDto);
		bool Update(int id, ShoppingCartItemDTO itemDto);
		bool Delete(int id);
	}
}
