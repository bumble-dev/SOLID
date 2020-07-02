using jsonBlog.Models;
using Microsoft.Extensions.Logging;

namespace jsonBlog
{
    public class PostLogger
    {
        private readonly ILogger _logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<BlogEngine>();

        public void SavedPost(Post savedPost) => _logger.LogInformation($"Saved post {savedPost.ID}");
        public void SavingPost(Post post) => _logger.LogInformation($"Saving post {post.ID}");
        public void ReturnPost(int id) => _logger.LogInformation($"Returning post {id}");
        public void NotFoundInCache(int id) => _logger.LogInformation($"Not found in cache post {id}");
        public void LoadingPost(int id) => _logger.LogInformation($"Loading post {id}");
    }
}