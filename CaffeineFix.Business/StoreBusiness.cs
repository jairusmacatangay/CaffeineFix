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

        public List<StoreDomainModel> GetProductsAutoComplete(string query)
        {
            List<StoreDomainModel> list = productRepository
                .GetAll(x => x.IsDeleted == false && x.ProductName.ToLower().Contains(query.ToLower()))
                .Select(x => new StoreDomainModel
                {
                    ProductName = x.ProductName
                }).ToList();

            return list;
        }

        public List<StoreDomainModel> SearchProduct(string search)
        {
            List<StoreDomainModel> productDM = productRepository
                .GetAll(x => x.ProductName.Contains(search) && x.IsDeleted == false)
                .Select(x => new StoreDomainModel
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    ImagePath = x.ProductImage.ImagePath
                })
                .Take(1)
                .ToList();

            return productDM;
        }

        public List<StoreDomainModel> FilterProductsBy(string filterOption)
        {
            List<StoreDomainModel> filteredList = new List<StoreDomainModel>();

            if (filterOption == "Coffee" ||
                filterOption == "Equipment" ||
                filterOption == "Drinkware")
            {
                filteredList = productRepository
                    .GetAll(x => x.ProductCategory.ProductCategoryName.Contains(filterOption) && x.IsDeleted == false)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return filteredList;
            }
            else if (filterOption == "Dark Roast" ||
                     filterOption == "Medium Roast" ||
                     filterOption == "Light Roast")
            {
                filteredList = productRepository
                    .GetAll(x => x.RoastLevel.RoastLevelLabel.Contains(filterOption) && x.IsDeleted == false)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return filteredList;
            }
            else if (filterOption == "Coffee Supplies" ||
                     filterOption == "Coffee Maker" ||
                     filterOption == "Grinder")
            {
                filteredList = productRepository
                    .GetAll(x => x.EquipmentType.EquipmentTypeLabel.Contains(filterOption) && x.IsDeleted == false)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return filteredList;
            }
            else
            {
                filteredList = productRepository
                    .GetAll(x => x.DrinkwareType.DrinkwareTypeLabel.Contains(filterOption) && x.IsDeleted == false)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return filteredList;
            }
        }

        public List<StoreDomainModel> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            List<StoreDomainModel> prcFltrdList = productRepository
                .GetAll(x => x.Price > minPrice && x.Price < maxPrice && x.IsDeleted == false)
                .Select(x => new StoreDomainModel
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    ImagePath = x.ProductImage.ImagePath
                }).ToList();

            return prcFltrdList;
        }

        public List<StoreDomainModel> SortProductsBy(string selectedOption)
        {
            List<StoreDomainModel> sortedProductsList = new List<StoreDomainModel>();

            if (selectedOption == "1")
            {
                sortedProductsList = productRepository
                    .GetAll(x => x.IsDeleted == false)
                    .OrderBy(x => x.Price)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return sortedProductsList;
            }
            else if (selectedOption == "2")
            {
                sortedProductsList = productRepository
                    .GetAll(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.Price)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return sortedProductsList;
            }
            else if (selectedOption == "3")
            {
                sortedProductsList = productRepository
                    .GetAll(x => x.IsDeleted == false)
                    .OrderBy(x => x.DateCreated)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return sortedProductsList;
            }
            else if (selectedOption == "4")
            {
                sortedProductsList = productRepository
                    .GetAll(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.DateCreated)
                    .Select(x => new StoreDomainModel
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        ImagePath = x.ProductImage.ImagePath
                    }).ToList();

                return sortedProductsList;
            }
            else
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
}
