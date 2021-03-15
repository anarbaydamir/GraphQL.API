using GraphQL.API.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.Repositories.Interface
{
    public interface IProductReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetForProduct(int productId);
        Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> products);
        Task<ProductReview> Add(ProductReview productReview);
    }
}
