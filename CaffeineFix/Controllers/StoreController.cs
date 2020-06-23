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
    }
}