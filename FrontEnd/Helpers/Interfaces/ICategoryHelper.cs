﻿using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ICategoryHelper
    {
        List<CategoryViewModel> GetAllCategories();
        CategoryViewModel GetCategoryById(int id);
    }
}
