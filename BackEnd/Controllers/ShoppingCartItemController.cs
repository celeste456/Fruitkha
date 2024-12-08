using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
	public class ShoppingCartItemController : Controller
	{
		private readonly IShoppingCartItemService _shoppingCartItemService;

		public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
		{
			_shoppingCartItemService = shoppingCartItemService;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var items = _shoppingCartItemService.GetAll();
			return Ok(items);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var item = _shoppingCartItemService.GetById(id);
			if (item == null)
				return NotFound();

			return Ok(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] ShoppingCartItemDTO itemDto)
		{
			if (_shoppingCartItemService.Create(itemDto))
				return CreatedAtAction(nameof(GetById), new { id = itemDto.Id }, itemDto);

			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, [FromBody] ShoppingCartItemDTO itemDto)
		{
			if (_shoppingCartItemService.Update(id, itemDto))
				return NoContent();

			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (_shoppingCartItemService.Delete(id))
				return NoContent();

			return NotFound();
		}
	}
}
