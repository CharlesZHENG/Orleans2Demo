using Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Tests
{
    public class BlogPostGrainTests
    {
        [Test]
        public async Task SaveCommentTest()
        {
            // arrange

            const int blogPostId = 1;

            var fixedDateTime = new DateTime(2018, 4, 29, 18, 28, 33, DateTimeKind.Utc);
            var mockRepo = new Mock<ICommentRepository>(MockBehavior.Strict);
            var mockTimeService = new Mock<ITimeService>(MockBehavior.Strict);

            mockRepo.Setup(x => x.SaveCommentAsync(blogPostId, It.IsAny<StoredComment>()))
                    .Returns(Task.CompletedTask);
            mockTimeService.Setup(x => x.UtcNow)
                           .Returns(fixedDateTime);

            var grain = new BlogPostGrain(mockRepo.Object, mockTimeService.Object);

            const string name = "George";
            const string emailAddress = "george@food.com";
            const string body = "I'm hungry!";

            var comment = new InputComment()
            {
                Name = name,
                EmailAddress = emailAddress,
                Body = body
            };

            // act

            await grain.SaveCommentAsync(blogPostId, comment);

            // assert

            mockRepo.Verify(x => x.SaveCommentAsync(blogPostId, It.Is<StoredComment>(
                c => c.Name == name
                  && c.EmailAddress == emailAddress
                  && c.Body == body
                  && c.Timestamp == fixedDateTime
            )));
        }
    }
}
