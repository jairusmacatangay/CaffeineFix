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
        private readonly ProductCategoryRepository productCategoryRepository;
        private readonly RoastLevelRepository roastLevelRepository;
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly DrinkwareTypeRepository drinkwareTypeRepository;

        public ProductsBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            productRepository = new ProductRepository(unitOfWork);
            productCategoryRepository = new ProductCategoryRepository(unitOfWork);
            roastLevelRepository = new RoastLevelRepository(unitOfWork);
            equipmentTypeRepository = new EquipmentTypeRepository(unitOfWork);
            drinkwareTypeRepository = new DrinkwareTypeRepository(unitOfWork);
        }

        public List<ProductDomainModel> GetAllProducts(int pageNo, int iDisplayLength, string sSearch)
        {
            List<ProductDomainModel> productsList = new List<ProductDomainModel>();

            if (sSearch != null)
            {
                productsList = productRepository.GetAll()
                    .Where(x => x.ProductName.Contains(sSearch) ||
                    x.ProductCategory.ProductCategoryName.Contains(sSearch))
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
            }
            else
            {
                productsList = productRepository.GetAll()
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
            }
            return productsList;
        }

        public int CountProducts(string sSearch)
        {
            if (sSearch != null)
            {
                return productRepository.GetAll()
                    .Where(x => x.ProductName.Contains(sSearch) ||
                    x.ProductCategory.ProductCategoryName.Contains(sSearch)).Count();
            }
            else
            {
                return productRepository.GetAll().Count();
            }
        }

        public int GetPageNo(int iDisplayStart, int iDisplayLength)
        {
            if (iDisplayStart >= iDisplayLength)
            {
                return (iDisplayStart / iDisplayLength) + 1;
            }
            return 1;
        }

        public List<ProductDomainModel> GetProduct(int productID)
        {
            List<ProductDomainModel> product = productRepository.GetAll().Where(x => x.ProductID == productID)
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

            return product;
        }

        public List<ProductCategoryDomainModel> GetCategories()
        {
            List<ProductCategoryDomainModel> categoryList = productCategoryRepository.GetAll()
                .Select(m => new ProductCategoryDomainModel
                {
                    ProductCategoryID = m.ProductCategoryID,
                    ProductCategoryName = m.ProductCategoryName
                }).ToList();

            return categoryList;
        }

        public ProductDomainModel GetProductForEdit(int productID)
        {
            ProductDomainModel model = new ProductDomainModel();

            if (productID > 0)
            {
                Product product = productRepository.SingleOrDefault(x => x.ProductID == productID);

                model.ProductID = product.ProductID;
                model.ProductName = product.ProductName;
                model.ProductCategoryID = product.ProductCategoryID;
                model.Description = product.Description;
                model.Price = product.Price;
                model.RoastLevelID = product.RoastLevelID;
                model.EquipmentTypeID = product.EquipmentTypeID;
                model.DrinkwareTypeID = product.DrinkwareTypeID;
            }

            return model;
        }

        public List<RoastLevelDomainModel> GetRoastLevelList(int productCategoryID)
        {
            List<RoastLevelDomainModel> roastLevelList = new List<RoastLevelDomainModel>();

            if (productCategoryID == 0)
            {
                roastLevelList = roastLevelRepository.GetAll()
                .Select(x => new RoastLevelDomainModel
                {
                    RoastLevelID = x.RoastLevelID,
                    RoastLevelLabel = x.RoastLevelLabel
                }).ToList();
            }
            else
            {
                roastLevelList = roastLevelRepository.GetAll()
                .Where(x => x.RoastLevelID != 0)
                .Select(x => new RoastLevelDomainModel
                {
                    RoastLevelID = x.RoastLevelID,
                    RoastLevelLabel = x.RoastLevelLabel
                }).ToList();
            }

            return roastLevelList;
        }

        public List<EquipmentTypeDomainModel> GetEquipmentTypesList(int productCategoryID)
        {
            List<EquipmentTypeDomainModel> equipmentTypeList = new List<EquipmentTypeDomainModel>();

            if (productCategoryID == 0)
            {
                equipmentTypeList = equipmentTypeRepository.GetAll()
                    .Select(x => new EquipmentTypeDomainModel
                    {
                        EquipmentTypeID = x.EquipmentTypeID,
                        EquipmentTypeLabel = x.EquipmentTypeLabel
                    }).ToList();
            }
            else
            {
                equipmentTypeList = equipmentTypeRepository.GetAll()
                    .Where(x => x.EquipmentTypeID != 0)
                    .Select(x => new EquipmentTypeDomainModel
                    {
                        EquipmentTypeID = x.EquipmentTypeID,
                        EquipmentTypeLabel = x.EquipmentTypeLabel
                    }).ToList();
            }

            return equipmentTypeList;
        }

        public List<DrinkwareTypeDomainModel> GetDrinkwareTypesList(int productCategoryID)
        {
            List<DrinkwareTypeDomainModel> drinkwareTypeList = new List<DrinkwareTypeDomainModel>();

            if (productCategoryID == 0)
            {
                drinkwareTypeList = drinkwareTypeRepository.GetAll()
                    .Select(x => new DrinkwareTypeDomainModel
                    {
                        DrinkwareTypeID = x.DrinkwareTypeID,
                        DrinkwareTypeLabel = x.DrinkwareTypeLabel
                    }).ToList();
            }
            else
            {
                drinkwareTypeList = drinkwareTypeRepository.GetAll()
                    .Where(x => x.DrinkwareTypeID != 0)
                    .Select(x => new DrinkwareTypeDomainModel
                    {
                        DrinkwareTypeID = x.DrinkwareTypeID,
                        DrinkwareTypeLabel = x.DrinkwareTypeLabel
                    }).ToList();
            }
            
            return drinkwareTypeList;
        }

        public void AddProduct(ProductDomainModel productDM)
        {
            Product product = new Product();
            product.ProductName = productDM.ProductName;
            product.ProductCategoryID = productDM.ProductCategoryID;
            product.Description = productDM.Description;
            product.Price = productDM.Price;

            if (productDM.RoastLevelID != null) { product.RoastLevelID = productDM.RoastLevelID; }
            else { product.RoastLevelID = 0; }

            if (productDM.EquipmentTypeID != null) { product.EquipmentTypeID = productDM.EquipmentTypeID; }
            else { product.EquipmentTypeID = 0; }

            if (product.DrinkwareTypeID != null) { product.DrinkwareTypeID = productDM.DrinkwareTypeID; }
            else { product.DrinkwareTypeID = 0; }          
            
            product.DateCreated = DateTime.Now;
            productRepository.Insert(product);
        }

        public void UpdateProduct(ProductDomainModel productDM)
        {
            Product product = productRepository.SingleOrDefault(x => x.ProductID == productDM.ProductID);
            product.ProductName = productDM.ProductName;
            product.ProductCategoryID = productDM.ProductCategoryID;
            product.Description = productDM.Description;
            product.Price = productDM.Price;

            if (productDM.RoastLevelID != null) { product.RoastLevelID = productDM.RoastLevelID; }
            else { product.RoastLevelID = 0; }

            if (productDM.EquipmentTypeID != null) { product.EquipmentTypeID = productDM.EquipmentTypeID; }
            else { product.EquipmentTypeID = 0; }

            if (productDM.DrinkwareTypeID != null) { product.DrinkwareTypeID = productDM.DrinkwareTypeID; }
            else { product.DrinkwareTypeID = 0; }

            product.DateLastModified = DateTime.Now;
            productRepository.Update(product);
        }
    }
}

