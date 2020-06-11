using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CaffeineFix.Domain;

namespace CaffeineFix.Business.Interface
{
    public interface IProductsBusiness
    {
        List<ProductDomainModel> GetAllProducts(int pageNo, int pageSize, string search);
        List<ProductDomainModel> SortByColumnWithOrder(string order, string orderDir, List<ProductDomainModel> productsDMList);
        int CountProducts(string search);
        int GetPageNo(int startRec, int pageSize);
        List<ProductDomainModel> GetProduct(int productID);
        List<ProductCategoryDomainModel> GetCategories();
        ProductDomainModel GetProductForEdit(int productID);
        List<RoastLevelDomainModel> GetRoastLevelList(int productCategoryID);
        List<EquipmentTypeDomainModel> GetEquipmentTypesList(int productCategoryID);
        List<DrinkwareTypeDomainModel> GetDrinkwareTypesList(int productCategoryID);
        void AddProduct(ProductDomainModel productDM);
        void UpdateProduct(ProductDomainModel productDM);
        void SaveImageData(ProductImageDomainModel modelDM);
        int GetRecentImageID();
        bool DeleteProduct(int productID);
    }
}
