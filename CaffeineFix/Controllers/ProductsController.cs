using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaffeineFix.Business.Interface;
using CaffeineFix.Domain;
using CaffeineFix.Models;

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
    }
}