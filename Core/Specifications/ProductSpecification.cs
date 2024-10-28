using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams productSpecParams) : base (x =>
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search))&&
                (productSpecParams.Brands.Count == 0 || productSpecParams.Brands.Contains(x.Brand) ) &&
                (productSpecParams.Types.Count == 0 || productSpecParams.Types.Contains(x.Type)) 
            ) //pass this expression
        {

            //pagesize = number of products in page
            //pageIndex is the page number
            //to calculate the skip products [Pagesize * (pageindex - 1)]
            //example pageSize = 5, PageIndex = 2 which means 5*(2-1) => skip 5, and take 5
            AddPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

            switch (productSpecParams.Sort) 
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
            
        
    }
}
