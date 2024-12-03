using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.APIModels;
using System.Text.Json;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
	public class ProductHelper : IProductHelper
	{
        IServiceRepository _repository;

        public ProductHelper(IServiceRepository Repository)
        {
            this._repository = Repository;
        }

        #region Conversiones

        ProductViewModel Convertir(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Photo = product.Photo != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(product.Photo)}" : null,
            };
        }

        private byte[] TryConvertBase64ToBytes(string base64String)
        {
            try
            {
                var base64Content = base64String.Split(',')[1];
                return Convert.FromBase64String(base64Content);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("Invalid Base64 format for Photo property.", ex);
            }
        }

        Product Convertir(ProductViewModel product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Photo = !string.IsNullOrEmpty(product.Photo) && product.Photo.Contains("base64,")
                ? TryConvertBase64ToBytes(product.Photo)
                : null,
            };
        }
        #endregion
        public ProductViewModel AddProduct(ProductViewModel productViewModel, IFormFile image)
        {
            try
            {
                var responseMessage = _repository.PostResponse("api/Product", productViewModel, image);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                }

                return productViewModel;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating the product.", ex);
            }
        }

        public ProductViewModel UpdateProduct(ProductViewModel productViewModel, IFormFile image)
        {
            try
            {
                var responseMessage = _repository.PutResponse("api/Product", productViewModel, image);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                }

                return productViewModel;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating the product.", ex);
            }
        }

        public List<ProductViewModel> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Product");

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }

            List<ProductViewModel> list = new List<ProductViewModel>();

            foreach (var item in products)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

        public ProductViewModel GetProductById(int id)
        {
            Product product = new Product();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Product/" + id.ToString());

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(content);
            }

            return Convertir(product);
        }

        public void DeleteProduct(int id)
        {
            HttpResponseMessage responseMessage = _repository.DeleteResponse("api/Product/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public List<ProductViewModel> GetProductsByCategory(int categoryId)
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Product/category/" + categoryId.ToString());

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }

            List<ProductViewModel> list = new List<ProductViewModel>();

            foreach (var item in products)
            {
                list.Add(Convertir(item));
            }

            return list;
        }

    }
}
