using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL.Types
{
    //    mutation($review: reviewInput!)
    //    {
    //        createReview(review: $review) { id title review}
    //    }
    //    {
    //  "review": {
    //  "title":"Hello test",
    //  "productId": 1
    //}
    //}
    //sample for add request
    public class ProductReviewInputType:InputObjectGraphType
    {
        public ProductReviewInputType()
        {
            Name = "reviewInput";
            Field<IdGraphType>("id");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("review");
            Field<NonNullGraphType<IntGraphType>>("productId");
        }
    }
}
