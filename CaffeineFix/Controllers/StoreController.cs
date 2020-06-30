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
            List<SortProductsByViewModel> sortList = new List<SortProductsByViewModel>();

            sortList.Add(new SortProductsByViewModel { SortID = 1, SortOption = "Price low to high" });
            sortList.Add(new SortProductsByViewModel { SortID = 2, SortOption = "Price high to low" });
            sortList.Add(new SortProductsByViewModel { SortID = 3, SortOption = "Recent" });
            sortList.Add(new SortProductsByViewModel { SortID = 4, SortOption = "Oldest" });

            ViewBag.SortOptions = new SelectList(sortList, "SortID", "SortOption");

            return View();
        }

        public ActionResult GetAllProducts(string view)
        {
            List<StoreDomainModel> storeDM = storeBusiness.GetAllProducts();
            List<StoreViewModel> storeVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(storeDM, storeVM);
            
            if (view == "grid")
            {
                return PartialView("_Products", storeVM);
            }
            else
            {
                return PartialView("_ProductsList", storeVM);
            }            
        }

        public JsonResult AutoCompleteProducts(string query)
{
            List<StoreDomainModel> listDM = storeBusiness.GetProductsAutoComplete(query);
            List<StoreViewModel> listVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(listDM, listVM);
            return Json(listVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchProduct(string search, string view)
        {
            List<StoreDomainModel> productDM = storeBusiness.SearchProduct(search);
            List<StoreViewModel> productVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(productDM, productVM);

            if (view == "grid")
            {
                return PartialView("_Products", productVM);
            }
            else
            {
                return PartialView("_ProductsList", productVM);
            }
        }

        public ActionResult FilterProductsBy(string filterOption, string view)
        {
            List<StoreDomainModel> filterdListDM = storeBusiness.FilterProductsBy(filterOption);
            List<StoreViewModel> filteredListVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(filterdListDM, filteredListVM);

            if (view == "grid")
            {
                return PartialView("_Products", filteredListVM);
            }
            else
            {
                return PartialView("_ProductsList", filteredListVM);
            }
            
        }

        public ActionResult FilterByPrice(decimal minPrice, decimal maxPrice, string view)
        {
            List<StoreDomainModel> prcFltrdLstDM = storeBusiness.FilterByPrice(minPrice, maxPrice);
            List<StoreViewModel> prcFltrdLstVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(prcFltrdLstDM, prcFltrdLstVM);

            if (view == "grid")
            {
                return PartialView("_Products", prcFltrdLstVM);
            }
            else
            {
                return PartialView("_ProductsList", prcFltrdLstVM);
            }
            
        }

        public ActionResult SortProductsBy(string selectedOption, string view)
        {
            List<StoreDomainModel> sortedProductsListDM = storeBusiness.SortProductsBy(selectedOption);
            List<StoreViewModel> sortedProductsListVM = new List<StoreViewModel>();
            AutoMapper.Mapper.Map(sortedProductsListDM, sortedProductsListVM);

            if (view == "grid")
            {
                return PartialView("_Products", sortedProductsListVM);
            }
            else
            {
                return PartialView("_ProductsList", sortedProductsListVM);
            }
        }
    }
}