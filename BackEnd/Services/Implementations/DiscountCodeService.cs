using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
	public class DiscountCodeService : IDiscountCodeService
	{
		IUnitOfWork _unit;
		IImageHandler _imageHandler;

		public DiscountCodeService(IUnitOfWork unitOfWork, IImageHandler imageHandler)
		{
			this._unit = unitOfWork;
			_imageHandler = imageHandler;
		}

		DiscountCodeDTO Convert(DiscountCode code)
		{
			return new DiscountCodeDTO
			{
				Id = code.Id,
				Code = code.Code,
				DiscountPercentage = code.DiscountPercentage,
				Photo = code.Photo
			};
		}

		DiscountCode Convert(DiscountCodeDTO code)
		{
			return new DiscountCode
			{
				Id = code.Id,
				Code = code.Code,
				DiscountPercentage = code.DiscountPercentage,
				Photo = code.Photo
			};
		}

		public DiscountCodeDTO Add(DiscountCodeDTO discountCodeDTO, IFormFile? image)
		{
			try
			{
				if (image != null)
				{
					discountCodeDTO.Photo = _imageHandler.ProcessImage(image);
				}

				_unit.DiscountCodeDAL.Add(Convert(discountCodeDTO));
				_unit.Complete();
				return discountCodeDTO;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error saving the discount.", ex);
			}
		}

		public List<DiscountCodeDTO> GetAll()
		{
			var discounts = _unit.DiscountCodeDAL.GetAll();
			List<DiscountCodeDTO> discountList = new List<DiscountCodeDTO>();
			foreach (var discount in discounts)
			{
				discountList.Add(Convert(discount));
			}
			return discountList;
		}

		public DiscountCodeDTO Update(DiscountCodeDTO DiscountCodeDTO, IFormFile? image)
		{
			try
			{
				if (image != null)
				{
					DiscountCodeDTO.Photo = _imageHandler.ProcessImage(image);
				}

				_unit.DiscountCodeDAL.Update(Convert(DiscountCodeDTO));
				_unit.Complete();
				return DiscountCodeDTO;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error updating the discount.", ex);
			}
		}

		public void Delete(int id)
		{
			DiscountCode code = new DiscountCode { Id = id };
			_unit.DiscountCodeDAL.Remove(code);
			_unit.Complete();
		}

		public DiscountCodeDTO GetById(int id)
		{
			var code = _unit.DiscountCodeDAL.Get(id);
			return Convert(code);
		}

	}
}
