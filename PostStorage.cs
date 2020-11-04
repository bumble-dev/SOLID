using System;
using System.IO;
using System.Threading.Tasks;
using jsonBlog.Models;

namespace jsonBlog
{
    public class PostStorage
    {
        public string Save(string serializedPost, Post post)
        {
            EnsureAuthorDirExists(post.Author);
            var path = GetPath(post.Author, post.ID);
            File.WriteAllText(path, serializedPost);
            return path;
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

        public string Load(User author, int id)
        {
            var path = GetPath(author, id);
            return File.ReadAllText(path);
        }

        public virtual string GetPath(User user, int id) => Path.Combine(user.Email, id + ".json");
    }
}