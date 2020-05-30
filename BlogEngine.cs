
using System;
using System.IO;
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

        public string SavePost(Post post)
        {
            _logger.LogInformation($"Saving post {post.ID}");
            if (!Directory.Exists(post.Author.Username))
            {
                Directory.CreateDirectory(post.Author.Username);
            }
            var path = Path.Combine(post.Author.Username, post.ID + ".json");
            var json = JsonSerializer.Serialize(post);
            File.WriteAllText(path,json);
            var savedPost = _postCache.Set(post.ID, post);

            _logger.LogInformation($"Saved post {savedPost.ID}" );
            return path;
        }

        public Post LoadPost(User user, int id)
        {
            _logger.LogInformation($"Loading post {id}");
            var path = Path.Combine(user.Email, id + ".json");

            var post = _postCache.GetOrCreate(id, _ => {
                _logger.LogInformation($"Not found in cache post {id}");
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<Post>(json); });

            _logger.LogInformation($"Returning post {id}");
            return post;
        }

    }
}