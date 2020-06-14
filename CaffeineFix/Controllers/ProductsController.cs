using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaffeineFix.Business.Interface;
using CaffeineFix.Domain;
using CaffeineFix.Models;
using CaffeineFix.Repository;

namespace CaffeineFix.Controllers
{
    public class ProductsController : Controller
    {
        IProductsBusiness productsBusiness;

        public ProductsController(IProductsBusiness _productsBusiness)
        {
            productsBusiness = _productsBusiness;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllProducts()
        {
            string search = Request.Form.GetValues("search[value]")[0];
            string draw = Request.Form.GetValues("draw")[0];
            string order = Request.Form.GetValues("order[0][column]")[0];
            string orderDir = Request.Form.GetValues("order[0][dir]")[0];
            int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
            int totalCount = 0;
            
            totalCount = productsBusiness.CountProducts(search);

            List<ProductDomainModel> productsDMList = productsBusiness.GetAllProducts(search);

            productsDMList = productsBusiness.SortByColumnWithOrder(order, orderDir, productsDMList);

            productsDMList = productsBusiness.ApplyPagination(startRec, pageSize, productsDMList);

            List<ProductViewModel> productsVMList = new List<ProductViewModel>();

            AutoMapper.Mapper.Map(productsDMList, productsVMList);

            return Json(new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = totalCount,
                recordsFiltered = totalCount,
                data = productsVMList
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewProduct(int productID)
        {
            ProductDomainModel productDM = productsBusiness.GetProduct(productID);

            ProductViewModel productVM = new ProductViewModel();

            AutoMapper.Mapper.Map(productDM, productVM);

            return PartialView("_ViewProduct", productVM);
        }

        public ActionResult LoadAddProductForm()
        {
            List<ProductCategoryDomainModel> categoryList = productsBusiness.GetCategories();
            ViewBag.CategoryList = new SelectList(categoryList, "ProductCategoryID", "ProductCategoryName");
            return PartialView("_AddProductForm");
        }

        public ActionResult LoadEditProductForm(int productID)
        {
            List<ProductCategoryDomainModel> categoryList = productsBusiness.GetCategories();
            List<RoastLevelDomainModel> roastLevelList = productsBusiness.GetRoastLevelList(1);
            List<EquipmentTypeDomainModel> equipmentTypeList = productsBusiness.GetEquipmentTypesList(1);
            List<DrinkwareTypeDomainModel> drinkwareTypeList = productsBusiness.GetDrinkwareTypesList(1);

            ViewBag.CategoryList = new SelectList(categoryList, "ProductCategoryID", "ProductCategoryName");
            ViewBag.RoastLevelList = new SelectList(roastLevelList, "RoastLevelID", "RoastLevelLabel");
            ViewBag.EquipmentTypeList = new SelectList(equipmentTypeList, "EquipmentTypeID", "EquipmentTypeLabel");
            ViewBag.DrinkwareTypeList = new SelectList(drinkwareTypeList, "DrinkwareTypeID", "DrinkwareTypeLabel");

            ProductDomainModel productDM = productsBusiness.GetProductForEdit(productID);

            ProductViewModel productVM = new ProductViewModel();

            AutoMapper.Mapper.Map(productDM, productVM);

            return PartialView("_EditProductForm", productVM);
        }

        public void AddProduct(ProductViewModel productVM)
        {
            int imageID = 0;

            if (productVM.ImageFile != null)
            {
                string filePath = string.Empty;
                string fileContentType = string.Empty;

                byte[] uploadedFile = new byte[productVM.ImageFile.InputStream.Length];
                productVM.ImageFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                fileContentType = productVM.ImageFile.ContentType;
                string folderPath = "/ProductsImage/";
                this.WriteBytesToFile(this.Server.MapPath(folderPath), uploadedFile, productVM.ImageFile.FileName);
                filePath = folderPath + productVM.ImageFile.FileName;

                string fullFilePath = "C:/Users/Jairus Macatangay/source/repos/CaffeineFix/CaffeineFix" + filePath;

                FileInfo fi = new FileInfo(fullFilePath);
                string imgSize = (fi.Length / 1024) + " KB";

                Bitmap bitmap = new Bitmap(fullFilePath);
                int imgHeight = bitmap.Height;
                int imgWidth = bitmap.Width;

                ProductImageDomainModel img = new ProductImageDomainModel();

                img.ImageName = productVM.ImageFile.FileName;
                img.ImageByte = uploadedFile;
                img.ImagePath = filePath;
                img.ImageExt = fileContentType;
                img.IsDeleted = false;
                img.ImageSize = imgSize;
                img.ImageHeight = imgHeight;
                img.ImageWidth = imgWidth;
                
                productsBusiness.SaveImageData(img);

                imageID = productsBusiness.GetRecentImageID();
            }

            productVM.ImageID = imageID;
            
            ProductDomainModel productDM = new ProductDomainModel();
            AutoMapper.Mapper.Map(productVM, productDM);
            productsBusiness.AddProduct(productDM);            
        }

        private void WriteBytesToFile(string rootFolderPath, byte[] fileBytes, string fileName)
        {
            if (!Directory.Exists(rootFolderPath))
            {
                string fullFolderPath = rootFolderPath;
                string folderPath = new Uri(fullFolderPath).LocalPath;
                Directory.CreateDirectory(folderPath);
            }

            string fullFilePath = rootFolderPath + fileName;

            using (var fs = System.IO.File.Create(fullFilePath))
            {
                fs.Flush();
                fs.Dispose();
                fs.Close();
            }
            
            BinaryWriter sw = new BinaryWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write));

            sw.Write(fileBytes);

            sw.Flush();
            sw.Dispose();
            sw.Close();
        }

        public void UpdateProduct(ProductViewModel productVM)
        {
            if (productVM.ImageFile != null)
            {
                string filePath = string.Empty;
                string fileContentType = string.Empty;

                byte[] uploadedFile = new byte[productVM.ImageFile.InputStream.Length];
                productVM.ImageFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                fileContentType = productVM.ImageFile.ContentType;
                string folderPath = "/ProductsImage/";
                this.WriteBytesToFile(this.Server.MapPath(folderPath), uploadedFile, productVM.ImageFile.FileName);
                filePath = folderPath + productVM.ImageFile.FileName;

                string fullFilePath = "C:/Users/Jairus Macatangay/source/repos/CaffeineFix/CaffeineFix" + filePath;

                FileInfo fi = new FileInfo(fullFilePath);
                string imgSize = (fi.Length / 1024) + " KB";

                Bitmap bitmap = new Bitmap(fullFilePath);
                int imgHeight = bitmap.Height;
                int imgWidth = bitmap.Width;

                ProductImageDomainModel img = new ProductImageDomainModel();

                img.ImageID = (int)productVM.ImageID;
                img.ImageName = productVM.ImageFile.FileName;
                img.ImageByte = uploadedFile;
                img.ImagePath = filePath;
                img.ImageExt = fileContentType;
                img.IsDeleted = false;
                img.ImageSize = imgSize;
                img.ImageHeight = imgHeight;
                img.ImageWidth = imgWidth;

                productsBusiness.SaveImageData(img);
            }

            ProductDomainModel productDM = new ProductDomainModel();
            AutoMapper.Mapper.Map(productVM, productDM);
            productsBusiness.UpdateProduct(productDM);
        }

        public ActionResult GetRoastLevelList(int productCategoryID)
        {
            List<RoastLevelDomainModel> subCategoryList = productsBusiness.GetRoastLevelList(productCategoryID);
            ViewBag.SubCategoryList = new SelectList(subCategoryList, "RoastLevelID", "RoastLevelLabel");

            return PartialView("_SubCategoryOptions");
        }

        public ActionResult GetEquipmentTypeList(int productCategoryID)
        {
            List<EquipmentTypeDomainModel> subCategoryList = productsBusiness.GetEquipmentTypesList(productCategoryID);
            ViewBag.SubCategoryList = new SelectList(subCategoryList, "EquipmentTypeID", "EquipmentTypeLabel");

            return PartialView("_SubCategoryOptions");
        }

        public ActionResult GetDrinkwareTypeList(int productCategoryID)
        {
            List<DrinkwareTypeDomainModel> subCategoryList = productsBusiness.GetDrinkwareTypesList(productCategoryID);
            ViewBag.SubCategoryList = new SelectList(subCategoryList, "DrinkwareTypeID", "DrinkwareTypeLabel");

            return PartialView("_SubCategoryOptions");
        }

        public JsonResult DeleteProduct(int productID)
        {
            bool result = productsBusiness.DeleteProduct(productID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}