using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeineFix.Domain
{
    public class StoreDomainModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ImagePath { get; set; }
    }
}
