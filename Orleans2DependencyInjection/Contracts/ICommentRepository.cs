using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICommentRepository
    {
        Task SaveCommentAsync(int blogPostId, StoredComment comment);
        Task<List<StoredComment>> GetCommentsAsync(int blogPostId);
    }
}
