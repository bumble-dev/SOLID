using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace jsonBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorName { get; set; }
        public string Body { get; set; }
        
    }
}