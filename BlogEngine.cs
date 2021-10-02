
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using jsonBlog.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace jsonBlog
{
    public interface IPostManager
    {
        Post Load(User user, int id);
        Post Save(Post post);
    }

    public partial class BlogEngine : IPostManager
    {
        private readonly PostLogger _logger = new PostLogger();
        private readonly IPostSerializer _serializer = new PostSerializer();
        private readonly IPostManager _storage;
        private readonly IPostCache _cache = new PostCache();

        public BlogEngine()
        {
            _storage = new PostStorageFileSystem(_serializer);
        }
        public Post Save(Post post)
        {
            _logger.SavingPost(post);
           
            _storage.Save(post);
            
            var savedPost = _cache.Set(post);

            _logger.SavedPost(savedPost);

            return post;
        }

        public Post Load(User user, int id)
        {
            _logger.LoadingPost(id);

            var post = _cache.GetOrCreate(id, _ =>
            {
                _logger.NotFoundInCache(id);
                return _storage.Load(user, id);
            });

            _logger.ReturnPost(id);
            return post;
        }
    }
}