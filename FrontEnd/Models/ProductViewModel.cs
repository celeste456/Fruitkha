using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public string Description { get; set; } = null!;

		public string? Photo { get; set; }

		public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
