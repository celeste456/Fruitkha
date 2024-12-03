using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _unit;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unit = unitOfWork;
        }

        CategoryDTO Convert(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        Category Convert(CategoryDTO category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _unit.CategoryDAL.GetAll();
            List<CategoryDTO> categoryList = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoryList.Add(Convert(category));
            }
            return categoryList;
        }

        public CategoryDTO GetById(int id)
        {
            var category = _unit.CategoryDAL.Get(id);
            return Convert(category);
        }
    }

}
