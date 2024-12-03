using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.APIModels;
using System.Text.Json;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
	public class ProductHelper : IProductHelper
	{
		#region
		IServiceRepository _repository;

		public ProductHelper(IServiceRepository serviceRepository)
		{
			this._repository = serviceRepository;
		}

		ProductViewModel Convertir(Product product)
		{
			return new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Photo = product.Photo != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(product.Photo)}" : null,
				CategoryId = product.CategoryId,
			};
		}

		Product Convertir(ProductViewModel product)
		{
			return new Product
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Photo = !string.IsNullOrEmpty(product.Photo) && product.Photo.StartsWith("data:image")
			    ? Convert.FromBase64String(product.Photo.Split(',')[1])
			    : null,
			CategoryId = product.CategoryId,
			};
		}
		#endregion
		public ProductViewModel AddProduct(ProductViewModel productViewModel)
        {
            HttpResponseMessage responseMessage = _repository.PostResponse("api/product", Convertir(productViewModel));
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
            return productViewModel;
        }

        public List<ProductViewModel> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/product");


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
            HttpResponseMessage responseMessage = _repository.GetResponse("api/product/" + id.ToString());

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(content);
            }

            return Convertir(product);
        }

        public ProductViewModel UpdateProduct(ProductViewModel productViewModel)
        {
            HttpResponseMessage responseMessage = _repository.PutResponse("api/product", Convertir(productViewModel));
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }

            return productViewModel;
        }

        public void DeleteProduct(int id)
        {
            HttpResponseMessage responseMessage = _repository.DeleteResponse("api/product" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }
    }
}
