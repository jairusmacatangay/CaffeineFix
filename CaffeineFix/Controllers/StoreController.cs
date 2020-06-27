using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaffeineFix.Domain;
using CaffeineFix.Business;
using CaffeineFix.Business.Interface;
using CaffeineFix.Models;

namespace CaffeineFix.Controllers
{
    public class StoreController : Controller
    {
        IStoreBusiness storeBusiness;

        public StoreController(IStoreBusiness _storeBusiness)
        {
            storeBusiness = _storeBusiness;
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult GetAllProducts()
        {
            List<StoreDomainModel> storeDM = storeBusiness.GetAllProducts();
            List<StoreViewModel> storeVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(storeDM, storeVM);
            return PartialView("_Products", storeVM);
        }

        public JsonResult AutoCompleteProducts(string query)
{
            List<StoreDomainModel> listDM = storeBusiness.GetProductsAutoComplete(query);
            List<StoreViewModel> listVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(listDM, listVM);
            return Json(listVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchProduct(string search)
        {
            List<StoreDomainModel> productDM = storeBusiness.SearchProduct(search);
            List<StoreViewModel> productVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(productDM, productVM);
            return PartialView("_Products", productVM);
        }

        public ActionResult FilterProductsBy(string filterOption)
        {
            List<StoreDomainModel> filterdListDM = storeBusiness.FilterProductsBy(filterOption);
            List<StoreViewModel> filteredListVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(filterdListDM, filteredListVM);
            return PartialView("_Products", filteredListVM);
        }
    }
}