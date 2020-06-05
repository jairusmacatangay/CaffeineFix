﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Domain;

namespace CaffeineFix.Business.Interface
{
    public interface IProductsBusiness
    {
        List<ProductDomainModel> GetAllProducts(int pageNo, int iDisplayLength, string sSearch);
        int CountProducts(string sSearch);
        int GetPageNo(int iDisplayStart, int iDisplayLength);
        List<ProductDomainModel> GetProduct(int productID);
        List<ProductCategoryDomainModel> GetCategories();
        ProductDomainModel GetProductForEdit(int productID);
        List<RoastLevelDomainModel> GetRoastLevelList(int productCategoryID);
        List<EquipmentTypeDomainModel> GetEquipmentTypesList(int productCategoryID);
        List<DrinkwareTypeDomainModel> GetDrinkwareTypesList(int productCategoryID);
        void AddProduct(ProductDomainModel productDM);
        void UpdateProduct(ProductDomainModel productDM);
    }
}
