using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Domain;

namespace CaffeineFix.Business.Interface
{
    public interface IStoreBusiness
    {
        List<StoreDomainModel> GetAllProducts();
        List<StoreDomainModel> GetProductsAutoComplete(string query);
        List<StoreDomainModel> SearchProduct(string search);
        List<StoreDomainModel> FilterProductsBy(string filterOption);
    }
}
