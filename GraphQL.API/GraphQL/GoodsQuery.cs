using GraphQL.API.GraphQL.Types;
using GraphQL.API.Repositories.Interface;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL
{
    public class GoodsQuery:ObjectGraphType
    {
        public GoodsQuery(IProductRepository productRepository)
        {
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.GetAll()
                );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return productRepository.GetById(id);
                });
        }
    }
}
