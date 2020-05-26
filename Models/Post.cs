using System;
using System.Collections.Generic;

namespace jsonBlog.Models
{

    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public User Author { get; set; }
        public string Body { get; set; }

        public override string ToString() =>  $"{ID} # {Title} \r\n==========================\r\n{Body}";
    }

    
}