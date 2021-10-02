using System.Text.Json;
using jsonBlog.Models;

namespace jsonBlog
{
    public interface IPostSerializer
    {
        Post DeserializePost(string serializedText);
        string SerializePost(Post post);
    }
    public class PostSerializer : IPostSerializer
    {
        public string SerializePost(Post post) => JsonSerializer.Serialize(post);
        public Post DeserializePost(string serializedText) => JsonSerializer.Deserialize<Post>(serializedText); 
    }
}