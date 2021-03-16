using GraphQL.API.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace GraphQL.API.GraphQL.Messaging
{
    public class ReviewMessageService
    {
        private readonly ISubject<ReviewAddedMessage> messageStream = new ReplaySubject<ReviewAddedMessage>(1);

        public ReviewAddedMessage AddReviewAddedMessage(ProductReview review)
        {
            var message = new ReviewAddedMessage
            {
                ProductId = review.ProductId,
                Title = review.Title
            };
            messageStream.OnNext(message);
            return message;
        }

        public IObservable<ReviewAddedMessage> GetMessages()
        {
            return messageStream.AsObservable();
        }
    }
}
