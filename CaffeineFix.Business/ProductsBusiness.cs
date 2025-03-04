﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        private readonly ProductImageRepository productImageRepository;

        public ProductsBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            productRepository = new ProductRepository(unitOfWork);
            productCategoryRepository = new ProductCategoryRepository(unitOfWork);
            roastLevelRepository = new RoastLevelRepository(unitOfWork);
            equipmentTypeRepository = new EquipmentTypeRepository(unitOfWork);
            drinkwareTypeRepository = new DrinkwareTypeRepository(unitOfWork);
            productImageRepository = new ProductImageRepository(unitOfWork);
        }

        public List<ProductDomainModel> GetAllProducts(string search)
        {
            List<ProductDomainModel> productsList = new List<ProductDomainModel>();

            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                productsList = productRepository.GetAll(x => x.IsDeleted == false)
                    .Where(x => x.IsDeleted == false && x.ProductName.ToLower().Contains(search.ToLower()) ||
                    x.ProductCategory.ProductCategoryName.ToLower().Contains(search.ToLower()))
                    .OrderBy(m => m.ProductID)
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
                        DrinkwareTypeLabel = m.DrinkwareType.DrinkwareTypeLabel,
                        IsDeleted = m.IsDeleted
                    }).ToList();
            }
            else
            {
                productsList = productRepository.GetAll(x => x.IsDeleted == false)
                    .Where(x => x.IsDeleted == false)
                    .OrderBy(m => m.ProductID)
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
                        DrinkwareTypeLabel = m.DrinkwareType.DrinkwareTypeLabel,
                        IsDeleted = m.IsDeleted
                    }).ToList();
            }

            return productsList;
        }

        public List<ProductDomainModel> SortByColumnWithOrder(string order, string orderDir,
            List<ProductDomainModel> productsDMList)
        {
            List<ProductDomainModel> list = new List<ProductDomainModel>();

            switch (order)
            {
                case "0":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                        productsDMList.OrderByDescending(x => x.ProductID).ToList() : 
                        productsDMList.OrderBy(x => x.ProductID).ToList();
                    break;
                case "1":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.ProductName).ToList() :
                        productsDMList.OrderBy(x => x.ProductName).ToList();
                    break;
                case "2":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.ProductCategoryName).ToList() :
                        productsDMList.OrderBy(x => x.ProductCategoryName).ToList();
                    break;
                case "3":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.Price).ToList() :
                        productsDMList.OrderBy(x => x.Price).ToList();
                    break;
                case "4":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.DateCreated).ToList() :
                        productsDMList.OrderBy(x => x.DateCreated).ToList();
                    break;
                case "5":
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.DateLastModified).ToList() :
                        productsDMList.OrderBy(x => x.DateLastModified).ToList();
                    break;
                default:
                    list = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                        productsDMList.OrderByDescending(x => x.ProductID).ToList() :
                        productsDMList.OrderBy(x => x.ProductID).ToList();
                    break;
            }

            return list;
        }

        public List<ProductDomainModel> ApplyPagination(int startRec, int pageSize, 
            List<ProductDomainModel> productsDMList)
        {
            List<ProductDomainModel> list = new List<ProductDomainModel>();
            list = productsDMList.Skip(startRec).Take(pageSize).ToList();
            return list;
        }

        public int CountProducts(string search)
        {
            if (search != null)
            {
                return productRepository.GetAll(x => x.IsDeleted == false)
                    .Where(x => x.IsDeleted == false && x.ProductName.ToLower().Contains(search.ToLower()) ||
                    x.ProductCategory.ProductCategoryName.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                return productRepository.GetAll(x => x.IsDeleted == false).Where(x => x.IsDeleted == false).Count();
            }
        }

        public ProductDomainModel GetProduct(int productID)
        {
            Product product = productRepository.SingleOrDefault(x => x.ProductID == productID);

            ProductDomainModel dm = new ProductDomainModel();

            dm.ProductID = product.ProductID;
            dm.ProductName = product.ProductName;
            dm.Description = product.Description;
            dm.Price = product.Price;
            dm.ProductCategoryName = product.ProductCategory.ProductCategoryName;
            dm.RoastLevelLabel = product.RoastLevel.RoastLevelLabel;
            dm.EquipmentTypeLabel = product.EquipmentType.EquipmentTypeLabel;
            dm.DrinkwareTypeLabel = product.DrinkwareType.DrinkwareTypeLabel;
            dm.ImagePath = product.ProductImage.ImagePath;

            return dm;
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
                model.ImageID = product.ImageID;
                model.ImagePath = product.ProductImage.ImagePath;
                model.ImageSize = product.ProductImage.ImageSize;
                model.ImageHeight = product.ProductImage.ImageHeight;
                model.ImageWidth = product.ProductImage.ImageWidth;
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
            product.ImageID = productDM.ImageID;
            product.Price = productDM.Price;

            if (productDM.RoastLevelID != null) { product.RoastLevelID = productDM.RoastLevelID; }
            else { product.RoastLevelID = 0; }

            if (productDM.EquipmentTypeID != null) { product.EquipmentTypeID = productDM.EquipmentTypeID; }
            else { product.EquipmentTypeID = 0; }

            if (productDM.DrinkwareTypeID != null) { product.DrinkwareTypeID = productDM.DrinkwareTypeID; }
            else { product.DrinkwareTypeID = 0; }          
            
            product.DateCreated = DateTime.Now;
            product.IsDeleted = false;
            productRepository.Insert(product);
        }

        public void UpdateProduct(ProductDomainModel productDM)
        {
            Product product = productRepository.SingleOrDefault(x => x.ProductID == productDM.ProductID);
            product.ProductName = productDM.ProductName;
            product.ProductCategoryID = productDM.ProductCategoryID;
            product.Description = productDM.Description;
            product.ImageID = productDM.ImageID;
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

        public void SaveImageData(ProductImageDomainModel modelDM)
        {
            ProductImage img;

            if (modelDM.ImageID > 0)
            {
                img = productImageRepository.SingleOrDefault(x => x.ImageID == modelDM.ImageID);

                img.ImageName = modelDM.ImageName;
                img.ImageByte = modelDM.ImageByte;
                img.ImagePath = modelDM.ImagePath;
                img.IsDeleted = modelDM.IsDeleted;
                img.ImageExt = modelDM.ImageExt;
                img.ImageSize = modelDM.ImageSize;
                img.ImageHeight = modelDM.ImageHeight;
                img.ImageWidth = modelDM.ImageWidth;

                productImageRepository.Update(img);
            }
            else
            {
                img = new ProductImage();

                img.ImageName = modelDM.ImageName;
                img.ImageByte = modelDM.ImageByte;
                img.ImagePath = modelDM.ImagePath;
                img.IsDeleted = modelDM.IsDeleted;
                img.ImageExt = modelDM.ImageExt;
                img.ImageSize = modelDM.ImageSize;
                img.ImageHeight = modelDM.ImageHeight;
                img.ImageWidth = modelDM.ImageWidth;

                productImageRepository.Insert(img);
            }            
        }

        public int GetRecentImageID()
        {
            return productImageRepository.GetAll().Select(x => x.ImageID).LastOrDefault();
        }

        public bool DeleteProduct(int productID)
        {
            bool result = false;

            Product product = productRepository.SingleOrDefault(x => x.IsDeleted == false && x.ProductID == productID);

            if (product != null)
            {
                product.IsDeleted = true;

                ProductImage image = productImageRepository.SingleOrDefault(
                    x => x.IsDeleted == false && 
                    x.ImageID == product.ImageID);

                image.IsDeleted = true;

                productRepository.Update(product);

                productImageRepository.Update(image);

                result = true;
            }

            return result;
        }
    }
}

