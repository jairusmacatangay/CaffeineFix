using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaffeineFix.Models
{
    public class StoreViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ImagePath { get; set; }
    }
}