using FrontEnd.Models;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductHelper
    {
		List<ProductViewModel> GetAllProducts();
		ProductViewModel GetProductById(int id);
        ProductViewModel AddProduct(ProductViewModel model, IFormFile image);
        ProductViewModel UpdateProduct(ProductViewModel model, IFormFile image);
        List<ProductViewModel> GetProductsByCategory(int categoryId);
        void DeleteProduct(int id);
	}
}
