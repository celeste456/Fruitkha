using Entities.Entities;

namespace BackEnd.DTO
{
	public class DiscountCodeDTO
	{
		public int Id { get; set; }

		public string Code { get; set; } = null!;

		public decimal DiscountPercentage { get; set; }

		public byte[]? Photo { get; set; }

	}
}
