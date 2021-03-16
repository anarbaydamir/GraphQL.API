using GraphQL.API.GraphQL.Messaging;
using GraphQL.API.GraphQL.Types;
using GraphQL.Resolvers;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL
{
    //    subscription {
    //  reviewAdded{
    //    productId,
    //    title
    //}
    //}
    //subscription sample
    public class GoodsSubscription:ObjectGraphType
    {
        public GoodsSubscription(ReviewMessageService messageService)
        {
            Name = "Subscription";
            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewAddedMessageType),
                Resolver = new FuncFieldResolver<ReviewAddedMessage>(c => c.Source as ReviewAddedMessage),
                Subscriber = new EventStreamResolver<ReviewAddedMessage>(c => messageService.GetMessages())
            });
        }
    }
}
