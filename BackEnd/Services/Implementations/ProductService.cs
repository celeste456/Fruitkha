using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region
        IUnitOfWork _unit;
        IImageHandler _imageHandler;

        public ProductService(IUnitOfWork unitOfWork, IImageHandler imageHandler)
        {
            this._unit = unitOfWork;
            _imageHandler = imageHandler;
        }

        ProductDTO Convert(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Photo = product.Photo,
            };
        }

        Product Convert(ProductDTO product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Photo = product.Photo,
            };
        }
        #endregion
        public ProductDTO Add(ProductDTO productDto, IFormFile? image)
        {
            try
            {
                if (image != null)
                {
                    productDto.Photo = _imageHandler.ProcessImage(image);
                }

                _unit.ProductDAL.Add(Convert(productDto));
                _unit.Complete();
                return productDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error saving the product.", ex);
            }
        }

        public List<ProductDTO> GetProducts()
        {
            var products = _unit.ProductDAL.GetAll();
            List<ProductDTO> productsList = new List<ProductDTO>();
            foreach (var product in products)
            {
                productsList.Add(Convert(product));
            }
            return productsList;
        }

        public ProductDTO Update(ProductDTO productDto, IFormFile? image)
        {
            try
            {
                if (image != null)
                {
                    productDto.Photo = _imageHandler.ProcessImage(image);
                }

                _unit.ProductDAL.Update(Convert(productDto));
                _unit.Complete();
                return productDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating the product.", ex);
            }
        }

        public void Delete(int id)
        {
            Product product = new Product { Id = id };
            _unit.ProductDAL.Remove(product);
            _unit.Complete();
        }

        public ProductDTO GetById(int id)
        {
            var product = _unit.ProductDAL.Get(id);
            return Convert(product);
        }

        public List<ProductDTO> GetProductsByCategory(int categoryId)
        {
            var products = _unit.ProductDAL.GetAll().Where(p => p.CategoryId == categoryId).ToList();
            return products.Select(p => Convert(p)).ToList();
        }
    }
}