using Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Silo
{
    public class MemoryCommentRepository : ICommentRepository
    {
        private ConcurrentDictionary<int, List<StoredComment>> dict;

        public MemoryCommentRepository()
        {
            this.dict = new ConcurrentDictionary<int, List<StoredComment>>();
        }

        public Task<List<StoredComment>> GetCommentsAsync(int blogPostId)
        {
            this.dict.TryGetValue(blogPostId, out var comments);
            return Task.FromResult(comments);
        }

        public Task SaveCommentAsync(int blogPostId, StoredComment comment)
        {
            this.dict.AddOrUpdate(blogPostId,
                addValue: new List<StoredComment>() { comment },
                updateValueFactory: (postId, commentsList) => {
                    commentsList.Add(comment);
                    return commentsList;
                });

            return Task.CompletedTask;
        }
    }
}
