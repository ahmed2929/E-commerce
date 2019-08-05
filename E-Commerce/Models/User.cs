using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_Commerce.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }





    }
}