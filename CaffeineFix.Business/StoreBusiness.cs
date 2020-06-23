using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeineFix.Business.Interface;
using CaffeineFix.Domain;
using CaffeineFix.Repository;
using CaffeineFix.Repository.Infrastructure.Contract;

namespace CaffeineFix.Business
{
    public class StoreBusiness : IStoreBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRepository productRepository;
        private readonly ProductImageRepository productImageRepository;

        public StoreBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            productRepository = new ProductRepository(unitOfWork);
            productImageRepository = new ProductImageRepository(unitOfWork);
        }

        public List<StoreDomainModel> GetAllProducts()
        {
            List<StoreDomainModel> productsList = productRepository
                .GetAll(x => x.IsDeleted == false)
                .Select(x => new StoreDomainModel
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    ImagePath = x.ProductImage.ImagePath
                }).ToList();

            return productsList;
        }
    }
}
