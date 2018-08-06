using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBlogPostGrain : IGrainWithIntegerKey
    {
        Task SaveCommentAsync(int blogPostId, InputComment comment);
        Task<List<StoredComment>> GetCommentsAsync(int blogPostId);
    }
}
