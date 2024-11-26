using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();
        ProductDTO Add(ProductDTO product, IFormFile? image);
        ProductDTO Update(ProductDTO product, IFormFile? image);
        void Delete(int id);
        ProductDTO GetById(int id);
        List<ProductDTO> GetProductsByCategory(int categoryId);

    }
}
