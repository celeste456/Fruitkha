﻿using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetCategories();
    }
}