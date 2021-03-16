using GraphQL.API.Data.Entitites;
using GraphQL.API.GraphQL.Types;
using GraphQL.API.Repositories.Interface;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL
{
    public class GoodsMutation:ObjectGraphType
    {
        public GoodsMutation(IProductReviewRepository productReviewRepository)
        {
            FieldAsync<ProductReviewType>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputType>> { Name = "review" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("review");
                    await productReviewRepository.Add(review);
                    return review;
                });
        }
    }
}
