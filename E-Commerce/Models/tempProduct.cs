using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class tempProduct
    {
        public int ID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal totalPrice { get; set; }
        public int quantaty { get; set; }
        public int Pcounter { get; set; }

        public tempProduct()
        {
            Pcounter = 0;
        }


    }
}