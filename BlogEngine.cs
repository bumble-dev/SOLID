
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using jsonBlog.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace jsonBlog
{
    public partial class BlogEngine
    {
        private readonly PostLogger _logger = new PostLogger();
        private readonly PostStorage _storage = new PostStorage();
        private readonly PostSerializer _serializer = new PostSerializer();
        private readonly PostCache _cache = new PostCache();
        

        public string SavePost(Post post)
        {
            _logger.SavingPost(post);
            var json = _serializer.SerializePost(post);
            var path = _storage.Save(json, post);
            var savedPost = _cache.Set(post);

            _logger.SavedPost(savedPost);
            return path;
        }
        
        public Post LoadPost(User user, int id)
        {
            _logger.LoadingPost(id);
           
            var post = _cache.GetOrCreate(id, _ => {
                _logger.NotFoundInCache(id);
                var json = _storage.Load(user, id);
                return _serializer.DeserializePost(json);
            });

            _logger.ReturnPost(id);
            return post;
        }
    }
}