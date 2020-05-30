using System;
using System.IO;
using System.Threading.Tasks;
using jsonBlog.Models;

namespace jsonBlog
{
    public class PostStorage
    {
        public string Save(string serializedPost, Post post){
            EnsureAuthorDirExists(post.Author);
            var path = GetPath(post.Author, post.ID);
            File.WriteAllText(path,serializedPost);
            return path;
        }

        private static void EnsureAuthorDirExists(User author)
        {
            if (!Directory.Exists(author.Username))
            {
                Directory.CreateDirectory(author.Username);
            }
        }

        public string Load(User author, int id){
            var path = GetPath(author, id);
            return File.ReadAllText(path); 
        }

        public string GetPath(User user, int id) => Path.Combine(user.Email, id + ".json");



    }
}