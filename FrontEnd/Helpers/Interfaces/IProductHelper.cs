using FrontEnd.Models;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductHelper
    {
		ProductViewModel AddProduct(ProductViewModel productViewModel);
		List<ProductViewModel> GetAllProducts();
		ProductViewModel GetProductById(int id);
		ProductViewModel UpdateProduct(ProductViewModel productViewModel);
		void DeleteProduct(int id);
	}
}
