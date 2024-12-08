using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
	public class ShoppingCartService : IShoppingCartService
	{
		#region
		private readonly IUnitOfWork _unitOfWork;

		public ShoppingCartService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		private ShoppingCartDTO ConvertToDTO(ShoppingCart cart)
		{
			return new ShoppingCartDTO
			{
				Id = cart.Id,
				UserId = cart.UserId,
				Items = cart.Items.Select(ConvertToDTO).ToList()
			};
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
		#endregion

		public ShoppingCartDTO GetCartByUserId(string userId)
		{
			var cart = _unitOfWork.ShoppingCartDAL.GetAll()
				.FirstOrDefault(c => c.UserId == userId);

			if (cart == null)
				return null;

			var items = _unitOfWork.ShoppingCartItemDAL.GetAll()
				.Where(i => i.ShoppingCartId == cart.Id)
				.ToList();

			foreach (var item in items)
			{
				item.Product = _unitOfWork.ProductDAL.Get(item.ProductId);
			}

			cart.Items = items;
			return ConvertToDTO(cart);
		}

		public bool AddItemToCart(string userId, ShoppingCartItemDTO itemDto)
		{
			var cart = _unitOfWork.ShoppingCartDAL.GetAll()
				.FirstOrDefault(c => c.UserId == userId);

			if (cart == null)
			{
				cart = new ShoppingCart { UserId = userId };
				_unitOfWork.ShoppingCartDAL.Add(cart);
				_unitOfWork.Complete();
			}

			var product = _unitOfWork.ProductDAL.Get(itemDto.ProductId);
			if (product == null)
				return false;

			var cartItem = ConvertToEntity(itemDto);
			cartItem.ShoppingCartId = cart.Id;
			cartItem.Product = product;

			_unitOfWork.ShoppingCartItemDAL.Add(cartItem);
			return _unitOfWork.Complete();
		}

		public bool UpdateItemInCart(string userId, ShoppingCartItemDTO itemDto)
		{
			var cart = _unitOfWork.ShoppingCartDAL.GetAll()
				.FirstOrDefault(c => c.UserId == userId);

			if (cart == null)
				return false;

			var cartItem = _unitOfWork.ShoppingCartItemDAL.Get(itemDto.Id);
			if (cartItem == null || cartItem.ShoppingCartId != cart.Id)
				return false;

			cartItem.Quantity = itemDto.Quantity;
			cartItem.ProductId = itemDto.ProductId;

			_unitOfWork.ShoppingCartItemDAL.Update(cartItem);

			return _unitOfWork.Complete();
		}


		public bool RemoveItemFromCart(string userId, int itemId)
		{
			var cart = _unitOfWork.ShoppingCartDAL.GetAll()
				.FirstOrDefault(c => c.UserId == userId);

			if (cart == null)
				return false;

			var cartItem = _unitOfWork.ShoppingCartItemDAL.Get(itemId);
			if (cartItem == null || cartItem.ShoppingCartId != cart.Id)
				return false;

			_unitOfWork.ShoppingCartItemDAL.Remove(cartItem);
			return _unitOfWork.Complete();
		}

		public decimal GetCartTotal(string userId)
		{
			var cart = _unitOfWork.ShoppingCartDAL.GetAll()
				.FirstOrDefault(c => c.UserId == userId);

			if (cart == null)
				return 0;

			var items = _unitOfWork.ShoppingCartItemDAL.GetAll()
				.Where(i => i.ShoppingCartId == cart.Id)
				.ToList();

			foreach (var item in items)
			{
				item.Product = _unitOfWork.ProductDAL.Get(item.ProductId);
			}

			return items.Sum(item => item.Quantity * item.Product.Price);
		}

	}

}
