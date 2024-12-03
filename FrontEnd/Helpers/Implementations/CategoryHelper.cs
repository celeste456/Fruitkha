using FrontEnd.Models;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.APIModels;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class CategoryHelper : ICategoryHelper
    {
        IServiceRepository _repository;

        public CategoryHelper(IServiceRepository Repository)
        {
            this._repository = Repository;
        }
        CategoryViewModel Convertir(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
        Category Convertir(CategoryViewModel category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public List<CategoryViewModel> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Category");

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(content);
            }

            List<CategoryViewModel> list = new List<CategoryViewModel>();

            foreach (var item in categories)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

        public CategoryViewModel GetCategoryById(int id)
        {
            Category category = new Category();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Category/" + id.ToString());

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(content);
            }

            return Convertir(category);
        }
    }
}
