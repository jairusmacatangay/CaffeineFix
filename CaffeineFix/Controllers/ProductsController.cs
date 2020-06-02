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
            List<ProductDomainModel> productsDMList = productsBusiness.GetAllProducts();

            List<ProductViewModel> productsVMList = new List<ProductViewModel>();

            AutoMapper.Mapper.Map(productsDMList, productsVMList);

            return Json(new
            {
                aaData = productsVMList,
                sEcho = param.sEcho,
                iTotalDisplayRecords = 3,
                iTotalRecords = 3
            }, JsonRequestBehavior.AllowGet);
        }
    }
}