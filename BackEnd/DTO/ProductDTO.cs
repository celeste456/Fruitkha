﻿namespace BackEnd.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public byte[]? Photo { get; set; }

        public int CategoryId { get; set; }

    }
}
