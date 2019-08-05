using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 999)]

        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImgURL { get; set; }
        public string description { get; set; }

    }
}