using Contracts;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Produces("application/json")]
    [Route("api/BlogPosts")]
    public class BlogPostsController : Controller
    {
        private IClusterClient orleansClient;

        public BlogPostsController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet]
        public Task<List<StoredComment>> Get(int blogPostId)
        {
            var grain = this.orleansClient.GetGrain<IBlogPostGrain>(blogPostId);
            return grain.GetCommentsAsync(blogPostId);
        }

        [HttpPut]
        public async Task Put(int blogPostId, InputComment comment)
        {
            var grain = this.orleansClient.GetGrain<IBlogPostGrain>(blogPostId);
            await grain.SaveCommentAsync(blogPostId, comment);
        }
    }
}
