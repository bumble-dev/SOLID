using Microsoft.Extensions.Logging;
using jsonBlog.Models;
using System.Threading.Tasks;

namespace jsonBlog
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Trace));
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Program started");

            var engine = new BlogEngine();

            var me = new User{ID = 1, Email = "evilznet@gmail.com", Name="Vincen", Phone= "+330123456789", Username= "evilz", Website = "http://www.evilznet.com"};

            var newPost = new Post{ ID=1, Author= me, Body="Some amazing content",Title="This is my first post"};

            var result = engine.SavePost(newPost);

            logger.LogInformation(result);

            var postLoaded = engine.LoadPost(me,newPost.ID);

            logger.LogInformation(postLoaded.ToString());
        }
    }
}
