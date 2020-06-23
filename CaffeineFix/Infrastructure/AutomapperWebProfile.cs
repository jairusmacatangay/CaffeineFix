using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaffeineFix.Domain;
using CaffeineFix.Models;

namespace CaffeineFix.Infrastructure
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<ProductDomainModel, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDomainModel>();

            CreateMap<StoreDomainModel, StoreViewModel>();
            CreateMap<StoreViewModel, StoreDomainModel>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();
            });
        }
    }
}