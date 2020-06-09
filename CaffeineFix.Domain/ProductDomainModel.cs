using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CaffeineFix.Domain
{
    public class ProductDomainModel
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

        public HttpPostedFileWrapper ImageFile { get; set; }

        public string ImagePath { get; set; }
        public string ImageSize { get; set; }
        public Nullable<int> ImageHeight { get; set; }
        public Nullable<int> ImageWidth { get; set; }
    }
}
