using System;
using System.IO;
using System.Threading.Tasks;
using jsonBlog.Models;

namespace jsonBlog
{
    public interface IFileLocator
    {
        string GetPath(User user, int id);
    }

  

    public class PostStorageFileSystem : IFileLocator, IPostManager
    {
        private readonly IPostSerializer _serializer;

        public PostStorageFileSystem(IPostSerializer serializer)
        {
            this._serializer = serializer;
        }
        public virtual Post Save(Post post)
        {
            EnsureAuthorDirExists(post.Author);
            var path = GetPath(post.Author, post.ID);
            var json = _serializer.SerializePost(post);
            File.WriteAllText(path, json);
            return post;

        }

        private void EnsureAuthorDirExists(User author)
        {
            var path = GetPath(author, 0);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public virtual Post Load(User author, int id)
        {
            var path = GetPath(author, id);

            var json = File.ReadAllText(path);
            return _serializer.DeserializePost(json);
        }

        public virtual string GetPath(User user, int id) => Path.Combine(user.Email, id + ".json");
    }
}