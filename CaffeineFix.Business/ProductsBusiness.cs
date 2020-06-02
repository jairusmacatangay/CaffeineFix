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
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRepository productRepository;

        public ProductsBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            productRepository = new ProductRepository(unitOfWork);
        }

        public List<ProductDomainModel> GetAllProducts(int pageNo, int iDisplayLength)
        {
            List<ProductDomainModel> productsList = productRepository.GetAll()
                .OrderBy(m => m.ProductID)
                .Skip((pageNo - 1) * iDisplayLength)
                .Take(iDisplayLength)
                .Select(m => new ProductDomainModel
                {
                    ProductID = m.ProductID,
                    ProductName = m.ProductName,
                    ProductCategoryID = m.ProductCategoryID,
                    Description = m.Description,
                    Price = m.Price,
                    ImageID = m.ImageID,
                    RoastLevelID = m.RoastLevelID,
                    EquipmentTypeID = m.EquipmentTypeID,
                    DrinkwareTypeID = m.DrinkwareTypeID,
                    DateCreated = m.DateCreated,
                    DateLastModified = m.DateLastModified,
                    ProductCategoryName = m.ProductCategory.ProductCategoryName,
                    RoastLevelLabel = m.RoastLevel.RoastLevelLabel,
                    EquipmentTypeLabel = m.EquipmentType.EquipmentTypeLabel,
                    DrinkwareTypeLabel = m.DrinkwareType.DrinkwareTypeLabel
                }).ToList();

            return productsList;
        }

        public int CountProducts()
        {
            return productRepository.GetAll().Count();
        }
    }
}

