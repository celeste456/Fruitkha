using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
	public class ShoppingCartItemService : IShoppingCartItemService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ShoppingCartItemService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		private ShoppingCartItemDTO ConvertToDTO(ShoppingCartItem item)
		{
			return new ShoppingCartItemDTO
			{
				Id = item.Id,
				ShoppingCartId = item.ShoppingCartId,
				ProductId = item.ProductId,
				Quantity = item.Quantity,
				TotalPrice = item.Quantity * item.Product.Price
			};
		}

		private ShoppingCartItem ConvertToEntity(ShoppingCartItemDTO itemDto)
		{
			return new ShoppingCartItem
			{
				Id = itemDto.Id,
				ShoppingCartId = itemDto.ShoppingCartId,
				ProductId = itemDto.ProductId,
				Quantity = itemDto.Quantity
			};
		}
		public IEnumerable<ShoppingCartItemDTO> GetAll()
		{
			var items = _unitOfWork.ShoppingCartItemDAL.GetAll();
			return items.Select(ConvertToDTO).ToList();
		}

		public ShoppingCartItemDTO GetById(int id)
		{
			var item = _unitOfWork.ShoppingCartItemDAL.Get(id);
			return item == null ? null : ConvertToDTO(item);
		}

		public bool Create(ShoppingCartItemDTO itemDto)
		{
			var product = _unitOfWork.ProductDAL.Get(itemDto.ProductId);
			if (product == null)
				return false;

			var item = ConvertToEntity(itemDto);
			item.Product = product;

			if (_unitOfWork.ShoppingCartItemDAL.Add(item))
			{
				_unitOfWork.Complete();
				return true;
			}

			return false;
		}

		public bool Update(int id, ShoppingCartItemDTO itemDto)
		{
			if (id != itemDto.Id)
				return false;

			var item = _unitOfWork.ShoppingCartItemDAL.Get(id);
			if (item == null)
				return false;

			item.Quantity = itemDto.Quantity;
			item.ProductId = itemDto.ProductId;

			if (_unitOfWork.ShoppingCartItemDAL.Update(item))
			{
				_unitOfWork.Complete();
				return true;
			}

			return false;
		}

		public bool Delete(int id)
		{
			var item = _unitOfWork.ShoppingCartItemDAL.Get(id);
			if (item == null)
				return false;

			if (_unitOfWork.ShoppingCartItemDAL.Remove(item))
			{
				_unitOfWork.Complete();
				return true;
			}

			return false;
		}
	}
}
