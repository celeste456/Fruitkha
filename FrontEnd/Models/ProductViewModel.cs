using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace FrontEnd.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public string Description { get; set; } = null!;

		public string? Photo { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

		public string? CategoryName;

    }
}
