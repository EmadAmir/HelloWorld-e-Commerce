﻿using System.ComponentModel.DataAnnotations;

namespace HelloWorldWebAPI.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0.01,double.MaxValue, ErrorMessage = "Price should be more than 0")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        [Required] 
        public string Type { get; set; } = string.Empty;
        [Required] 
        public string Brand { get; set; } = string.Empty;
        [Range(1,int.MaxValue,ErrorMessage = "Quantity in stock must be greater than 1")]
        public int QuantityInStock { get; set; }
    }
}