using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL.Types
{
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
