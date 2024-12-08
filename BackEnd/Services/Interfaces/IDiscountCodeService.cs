using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
	public interface IDiscountCodeService
	{
		List<DiscountCodeDTO> GetAll();
		DiscountCodeDTO Add(DiscountCodeDTO code, IFormFile? image);
		DiscountCodeDTO Update(DiscountCodeDTO code, IFormFile? image);
		void Delete(int id);
		DiscountCodeDTO GetById(int id);
	}
}
