using GraphQL.Types;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL
{
    public class GoodsSchema:Schema
    {
        public GoodsSchema(IServiceProvider provider):base(provider)
        {
            Query = (IObjectGraphType)provider.GetService(typeof(GoodsQuery));
        }
    }
}
