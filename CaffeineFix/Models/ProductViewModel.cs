using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaffeineFix.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please provide a name")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Must be 5-50 characters long", MinimumLength = 5)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public Nullable<int> ProductCategoryID { get; set; }

        [Required(ErrorMessage = "Please provide a description")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Must be 20-500 characters long", MinimumLength = 20)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide a price")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }

        public Nullable<int> ImageID { get; set; }

        [Required(ErrorMessage = "Please select a sub-category")]
        [Display(Name = "Sub-Category")]
        public Nullable<int> RoastLevelID { get; set; }

        [Required(ErrorMessage = "Please select a sub-category")]
        [Display(Name = "Sub-Category")]
        public Nullable<int> EquipmentTypeID { get; set; }

        [Required(ErrorMessage = "Please select a sub-category")]
        [Display(Name = "Sub-Category")]
        public Nullable<int> DrinkwareTypeID { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateLastModified { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public string ProductCategoryName { get; set; }
        public string RoastLevelLabel { get; set; }
        public string EquipmentTypeLabel { get; set; }
        public string DrinkwareTypeLabel { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        public string ImagePath { get; set; }
        public string ImageSize { get; set; }
        public Nullable<int> ImageHeight { get; set; }
        public Nullable<int> ImageWidth { get; set; }
    }
}