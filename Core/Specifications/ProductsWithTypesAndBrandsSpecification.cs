using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
              : base(x => 
                   (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                   (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                   (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) 
 
              )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
           // AddInclude(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                 switch(productParams.Sort)
                 {
                    case "priceAsc":
                       AddOrderBy(p => p.Price);
                       break;
                    case "priceDesc":
                       AddOrderByDesceding(p => p.Price);
                       break;   
                    default:
                        AddOrderBy(x => x.Name); 
                        break;
                 }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}