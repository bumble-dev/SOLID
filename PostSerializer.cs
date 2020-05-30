using System.Text.Json;
using jsonBlog.Models;

namespace jsonBlog
{
    public class PostSerializer
    {
        public string SerializePost(Post post) => JsonSerializer.Serialize(post);
        public Post DeserializePost(string serializedText) => JsonSerializer.Deserialize<Post>(serializedText); 
    }
}