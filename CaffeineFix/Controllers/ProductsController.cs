using System;
using System.Collections.Generic;
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

        public JsonResult GetAllProducts(DataTablesParam param)
        {
            int pageNo = 1;
            int totalCount = 0;
            int iDisplayStart = param.iDisplayStart;
            int iDisplayLength = param.iDisplayLength;
            string sSearch = param.sSearch;

            pageNo = productsBusiness.GetPageNo(iDisplayStart, iDisplayLength);

            totalCount = productsBusiness.CountProducts(sSearch);

            List<ProductDomainModel> productsDMList = productsBusiness.GetAllProducts(pageNo, iDisplayLength, sSearch);

            List<ProductViewModel> productsVMList = new List<ProductViewModel>();

            AutoMapper.Mapper.Map(productsDMList, productsVMList);

            return Json(new
            {
                aaData = productsVMList,
                sEcho = param.sEcho,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = totalCount
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewProduct(int productID)
        {
            List<ProductDomainModel> productDM = productsBusiness.GetProduct(productID);

            List<ProductViewModel> productVM = new List<ProductViewModel>();

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
            ProductDomainModel productDM = new ProductDomainModel();
            AutoMapper.Mapper.Map(productVM, productDM);
            productsBusiness.AddProduct(productDM);            
        }

        public void UpdateProduct(ProductViewModel productVM)
        {
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
    }
}