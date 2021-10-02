using System;
using jsonBlog.Models;
using Microsoft.Extensions.Caching.Memory;

namespace jsonBlog
{
    public interface IPostCache
    {
        Post GetOrCreate(int id, Func<ICacheEntry, Post> factory);
        Post Set(Post post);
    }
    public class PostCache : IPostCache
    {
        private readonly IMemoryCache _postCache = new MemoryCache(new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.Zero
        });

        public Post Set(Post post) => _postCache.Set(post.ID, post);
        public Post GetOrCreate(int id, Func<ICacheEntry, Post> factory) => _postCache.GetOrCreate(id, factory);
    }
}