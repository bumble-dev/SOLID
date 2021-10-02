using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using jsonBlog.Models;

namespace jsonBlog
{
    public class PostStorageFileSystemB64Id : PostStorageFileSystem
    {
        public PostStorageFileSystemB64Id(PostSerializer serializer) : base(serializer) { }
        public override string GetPath(User user, int id)
        {
            var b = Encoding.ASCII.GetBytes(user.ID.ToString());
            var dir = Convert.ToBase64String(b);
            return Path.Combine(dir, $"{id}.json");
        }
    }
}