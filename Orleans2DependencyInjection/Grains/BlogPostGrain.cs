using Contracts;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class BlogPostGrain : Grain, IBlogPostGrain
    {
        private ICommentRepository repo;
        private ITimeService time;

        public BlogPostGrain(ICommentRepository repo, ITimeService time)
        {
            this.repo = repo;
            this.time = time;
        }

        public Task SaveCommentAsync(int blogPostId, InputComment comment)
        {
            var storedComment = new StoredComment()
            {
                Name = comment.Name,
                EmailAddress = comment.EmailAddress,
                Body = comment.Body,
                Timestamp = this.time.UtcNow
            };

            return this.repo.SaveCommentAsync(blogPostId, storedComment);
        }

        public Task<List<StoredComment>> GetCommentsAsync(int blogPostId)
            => this.repo.GetCommentsAsync(blogPostId);
    }
}
