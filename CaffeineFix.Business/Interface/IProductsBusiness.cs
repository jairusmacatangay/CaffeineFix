using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Domain;

namespace CaffeineFix.Business.Interface
{
    public interface IProductsBusiness
    {
        List<ProductDomainModel> GetAllProducts(int pageNo, int iDisplayLength);
        int CountProducts();
    }
}
