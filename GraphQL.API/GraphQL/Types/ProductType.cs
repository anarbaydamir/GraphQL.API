using GraphQL.API.Data.Entitites;
using GraphQL.API.Repositories.Interface;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(IProductReviewRepository productReviewRepository,
            IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("name of product");
            Field(t => t.Description);
            Field(t => t.IntroducedAt);
            Field(t => t.PhotoFileName);
            Field(t => t.Price);
            Field(t => t.Rating);
            Field(t => t.Stock);
            Field<ProductTypeEnumType>("Type", "type of product");

            Field<ListGraphType<ProductReviewType>>(
                "Reviews",
                resolve: context =>
                {
                    var loader =
                        dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", productReviewRepository.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
