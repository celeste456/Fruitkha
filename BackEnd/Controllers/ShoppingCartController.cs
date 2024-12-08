using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IShoppingCartService _shoppingCartService;

		public ShoppingCartController(IShoppingCartService shoppingCartService)
		{
			_shoppingCartService = shoppingCartService;
		}

		[HttpGet("{userId}")]
		public IActionResult GetCart(string userId)
		{
			var cart = _shoppingCartService.GetCartByUserId(userId);
			if (cart == null)
				return NotFound("Cart not found.");

			return Ok(cart);
		}

		[HttpPost("{userId}/items")]
		public IActionResult AddItem(string userId, ShoppingCartItemDTO itemDto)
		{
			if (_shoppingCartService.AddItemToCart(userId, itemDto))
				return Ok();

			return BadRequest("Unable to add item to cart.");
		}

		[HttpPut("{userId}/items")]
		public IActionResult UpdateItem(string userId, ShoppingCartItemDTO itemDto)
		{
			if (_shoppingCartService.UpdateItemInCart(userId, itemDto))
				return Ok();

			return BadRequest("Unable to update item.");
		}

		[HttpDelete("{userId}/items/{itemId}")]
		public IActionResult RemoveItem(string userId, int itemId)
		{
			if (_shoppingCartService.RemoveItemFromCart(userId, itemId))
				return Ok();

			return BadRequest("Unable to remove item.");
		}

		[HttpGet("{userId}/total")]
		public IActionResult GetTotal(string userId)
		{
			var total = _shoppingCartService.GetCartTotal(userId);
			return Ok(new { Total = total });
		}

	}
}
