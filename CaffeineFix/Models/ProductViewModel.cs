using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaffeineFix.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> ProductCategoryID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<int> RoastLevelID { get; set; }
        public Nullable<int> EquipmentTypeID { get; set; }
        public Nullable<int> DrinkwareTypeID { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateLastModified { get; set; }

        public string ProductCategoryName { get; set; }
        public string RoastLevelLabel { get; set; }
        public string EquipmentTypeLabel { get; set; }
        public string DrinkwareTypeLabel { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}