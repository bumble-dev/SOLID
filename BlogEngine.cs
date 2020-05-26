
using System;
using System.Text.Json;
using System.Threading.Tasks;
using jsonBlog.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace jsonBlog
{
    public class BlogEngine
    {
        private readonly IMemoryCache _postCache = new MemoryCache(new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.Zero
        });
        private readonly ILogger _logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<BlogEngine>();

        public async Task<string> SavePostAsync(Post post)
        {
            _logger.LogInformation($"Saving post {post.ID}");
            if (!Directory.Exists(post.Author.Username))
            {
                Directory.CreateDirectory(post.Author.Username);
            }
            var path = Path.Combine(post.Author.Username, post.ID + ".json");
            using (FileStream fs = File.Create(path))
            {
                await JsonSerializer.SerializeAsync(fs, post);
            }
            var savedPost = _postCache.Set(post.ID, post);

            _logger.LogInformation($"Saved post {savedPost.ID}" );
            return path;
        }

        public async Task<Post> LoadPostAsync(User user, int id)
        {
            _logger.LogInformation($"Loading post {id}");
            var path = Path.Combine(user.Email, id + ".json");

            var post = await _postCache.GetOrCreateAsync(id, async _ => {
                _logger.LogInformation($"Not found in cache post {id}");
                using var fs = File.OpenRead(path);
                var p = await JsonSerializer.DeserializeAsync<Post>(fs);
                return p; });

            _logger.LogInformation($"Returning post {id}");
            return post;
        }

    }
}