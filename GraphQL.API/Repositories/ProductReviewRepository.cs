using GraphQL.API.Data;
using GraphQL.API.Data.Entitites;
using GraphQL.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly AppDbContext dbContext;

        public ProductReviewRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductReview> Add(ProductReview productReview)
        {
            await dbContext.ProductReviews.AddAsync(productReview);
            await dbContext.SaveChangesAsync();
            return productReview;
        }

        public async Task<IEnumerable<ProductReview>> GetForProduct(int productId)
        {
            return await dbContext.ProductReviews.Where(pr => pr.ProductId == productId).ToListAsync();
        }

        public async Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> products)
        {
            var reviews = await dbContext.ProductReviews.Where(pr => products.Contains(pr.ProductId)).ToListAsync();
            return reviews.ToLookup(pr => pr.ProductId);
        }
    }
}
