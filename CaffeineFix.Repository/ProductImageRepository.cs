using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Repository.Infrastructure.Contract;

namespace CaffeineFix.Repository
{
    public class ProductImageRepository : Infrastructure.BaseRepository<ProductImage>
    {
        public ProductImageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
